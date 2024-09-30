using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IPropertyImageAppService
    {
        Task<RequestResult<bool>> AddImageToPropertyAsync(int propertyId, string fileName);
    }
}
