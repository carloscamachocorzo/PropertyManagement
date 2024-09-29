using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Million.PropertyManagement.Application.DependencyInjection;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;
using NLog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Million.PropertyManagement.Api
{
    public class Program
    {
        /// <summary>
        /// Nombre API
        /// </summary>
        private const string _APINAME = "Gestor de propiedades";
        public IConfiguration Configuration { get; }
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
              
          builder.Services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo
              {
                  Title = $"Davinci - {_APINAME}",
                  Version = "Version inicial",
                  Description = "Servicios para novedades (Contributivo y Subsidiado)",
                  Contact = new OpenApiContact
                  {
                      Name = "Ophelia Suite"
                  }
              });
              c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

              c.OperationFilter<AddResponseHeadersFilter>();
              c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
              c.OperationFilter<SecurityRequirementsOperationFilter>();
              ////Agregando comentarios Xml a la documentación
              //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              //c.IncludeXmlComments(xmlPath);

              c.EnableAnnotations();
              // Filtra tipos innecesarios
              c.CustomSchemaIds(type => type.FullName); // Ayuda a evitar conflictos de nombres de clases
              //c.SchemaFilter<CustomSchemaFilter>(); // Filtro para manejar tipos genéricos complejos
          });
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
public class CustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Aquí puedes manejar tipos genéricos como RequestResult<Property>
        if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(RequestResult<>) ||
            context.Type.Namespace.StartsWith("Microsoft.EntityFrameworkCore"))
        {
            schema.Properties.Clear();
            schema.Properties.Add("data", new OpenApiSchema { Type = "object" });
            // Agregar más propiedades según sea necesario
        }
    }
}