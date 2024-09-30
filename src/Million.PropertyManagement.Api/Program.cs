using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Million.PropertyManagement.Application.DependencyInjection;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;
using NLog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
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
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddFile("Logs/myapp-{Date}.txt"); // Necesitarás agregar un paquete NuGet para el logging en archivos

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
                    Title = $"MILLION - {_APINAME}",
                    Version = "Version inicial",
                    Description = "Servicios para gestion de propiedades Hoteleras (https://www.millioncareers.com.co/)",
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Camacho"
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();


                c.EnableAnnotations();
                // Filtra tipos innecesarios
                c.CustomSchemaIds(type => type.FullName); // Ayuda a evitar conflictos de nombres de clases

                // Configurar JWT en Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce 'Bearer' [espacio] y luego tu token en el cuadro de texto a continuación.\n\nEjemplo: \"Bearer 12345abcdef\""
                });

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
            });

            //// Agrega la autenticación JWT
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            //        ValidAudience = builder.Configuration["JwtSettings:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
            //        ClockSkew = TimeSpan.Zero  // Reduce la tolerancia en la validación de expiración del token
            //    };
            //});
            // Autenticación con Jwt
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                            ValidAudience = builder.Configuration["JwtSettings:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                        };
                    });

            //builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Habilitar autenticación y autorización en la aplicación
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
