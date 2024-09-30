using Microsoft.Extensions.DependencyInjection;
using Million.PropertyManagement.Application.Automapper;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.Repositories;
using Million.PropertyManagement.Infrastructure.Security;

namespace Million.PropertyManagement.Application.DependencyInjection
{
    public static class DependencyInjectionProfile
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper
            services.AddAutoMapper(typeof(GlobalMapperProfile));
            // Registrar servicios de la capa Application            
            services.AddScoped<ICreatePropertyAppService, CreatePropertyAppService>();
            services.AddScoped<IAuthAppService, AuthAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IPropertyImageAppService, PropertyImageAppService>();
            // Registrar servicios de la capa Domain
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();

            return services;
        }
    }
}
