using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface ICreatePropertyAppService
    {
        Task<Property> ExecuteAsync(PropertyDto property);
    }
}