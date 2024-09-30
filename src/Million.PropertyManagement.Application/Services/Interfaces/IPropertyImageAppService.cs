using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IPropertyImageAppService
    {
        /// <summary>
        /// Agrega una imagen a una propiedad específica.
        /// </summary>
        /// <param name="propertyId">El ID de la propiedad a la que se le va a agregar la imagen.</param>
        /// <param name="fileName">El nombre del archivo de la imagen a agregar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que representa la operación asíncrona, 
        /// con un <see cref="RequestResult{bool}"/> que indica el resultado de la operación.
        /// </returns>
        Task<RequestResult<bool>> AddImageToPropertyAsync(int propertyId, string fileName);
    }
}
