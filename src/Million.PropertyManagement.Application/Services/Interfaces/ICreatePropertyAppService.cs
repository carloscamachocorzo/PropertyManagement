using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface ICreatePropertyAppService
    {
        Task<RequestResult<Property>> ExecuteAsync(PropertyDto property); 
    }
}