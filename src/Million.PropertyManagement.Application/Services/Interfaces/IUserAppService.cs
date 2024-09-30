using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IUserAppService
    {
        /// <summary>
        /// Crea un nuevo usuario con los datos proporcionados.
        /// </summary>
        /// <param name="request">El DTO que contiene los datos del usuario a registrar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que representa la operación asíncrona, 
        /// con un <see cref="RequestResult{bool}"/> que indica el resultado de la operación.
        /// </returns>
        Task<RequestResult<bool>> CreateUserAsync(RegisterUserRequestDto request);
    }
}
