using Microsoft.Extensions.DependencyInjection;
using Million.PropertyManagement.Application.Automapper;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.Repositories;

namespace Million.PropertyManagement.Application.DependencyInjection
{
    public static class DependencyInjectionProfile
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper
            services.AddAutoMapper(typeof(GlobalMapperProfile));
            // Registrar servicios de la capa Application
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<ICreatePropertyAppService, CreatePropertyAppService>();
            
            //services.AddScoped<IOwnerService, OwnerService>();

            

            // Aquí puedes registrar otros servicios, como repositorios, UnitOfWork, etc.
            // services.AddScoped<IRepository, Repository>();
            // services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
