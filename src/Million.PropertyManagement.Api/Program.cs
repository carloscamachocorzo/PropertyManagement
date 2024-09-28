using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Application.DependencyInjection;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;
using NLog;

namespace Million.PropertyManagement.Api
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura la cadena de conexión
            builder.Services.AddDbContext<PropertyManagementContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var logger = LogManager.Setup().LoadConfigurationFromFile(String.Concat(AppDomain.CurrentDomain.BaseDirectory, "nlog.config")).GetCurrentClassLogger();


            // Agregar servicios de la capa de Application
            builder.Services.AddApplicationServices();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}