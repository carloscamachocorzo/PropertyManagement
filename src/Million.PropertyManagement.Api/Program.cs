using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Million.PropertyManagement.Application.DependencyInjection;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;
using NLog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Million.PropertyManagement.Api
{
    public class Program
    {
        /// <summary>
        /// Nombre API
        /// </summary>
        private const string _APINAME = "Gestor de propiedades";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
                builder.AddFile("Logs/myapp-{Date}.txt");
            });

            Microsoft.Extensions.Logging.ILogger loggerData = loggerFactory.CreateLogger<Program>();

            // Configura la cadena de conexión
            builder.Services.AddDbContext<PropertyManagementContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var logger = LogManager.Setup().LoadConfigurationFromFile(String.Concat(AppDomain.CurrentDomain.BaseDirectory, "nlog.config")).GetCurrentClassLogger();

            // Agregar servicios de la capa de Application
            builder.Services.AddApplicationServices();
            builder.Services.AddControllers();

            // Configurar Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"MILLION - {_APINAME}",
                    Version = "v1",
                    Description = "Servicios para gestion de propiedades Hoteleras",
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Camacho"
                    }
                });

                // Esquema de seguridad para JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce 'Bearer' [espacio] seguido del token JWT."
                });

                // Requisito de seguridad global para aplicar JWT a todas las operaciones
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // Otros filtros adicionales
                //c.OperationFilter<AddResponseHeadersFilter>();
                //c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.EnableAnnotations();
                c.CustomSchemaIds(type => type.FullName);
            });

           
            // Authorization
            var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
            var keyIssuer = builder.Configuration["JwtSettings:Issuer"];
            var keyAudience = builder.Configuration["JwtSettings:Audience"];
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = keyIssuer,
                    ValidAudience = keyAudience,
                    ClockSkew = TimeSpan.Zero
                };
            });
            var app = builder.Build();

            // Configurar el pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Habilitar enrutamiento
            app.UseRouting();

            // Habilitar autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Configurar los endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Ejecutar la aplicación
            app.Run();
        }
    }
}
