using Microsoft.Extensions.DependencyInjection;
using Million.PropertyManagement.Application.Automapper;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.Services.Interfaces;

namespace Million.PropertyManagement.Application.DependencyInjection
{
    public static class DependencyInjectionProfile
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar servicios de la capa Application
            services.AddScoped<ICreatePropertyAppService, CreatePropertyAppService>();
            //services.AddScoped<IOwnerService, OwnerService>();

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(GlobalMapperProfile));

            // Aquí puedes registrar otros servicios, como repositorios, UnitOfWork, etc.
            // services.AddScoped<IRepository, Repository>();
            // services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
