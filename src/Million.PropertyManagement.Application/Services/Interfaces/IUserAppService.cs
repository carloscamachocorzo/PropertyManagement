using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IUserAppService
    {        
        Task<RequestResult<bool>> CreateUserAsync(RegisterUserRequestDto request);
    }
}
