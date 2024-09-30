using Million.PropertyManagement.Application.Dtos.Property;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IPropertyAppService
    {
        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>
        /// <param name="propertyDto">Objeto con los datos de la propiedad a crear.</param>
        /// <returns>Resultado de la creación, incluyendo la propiedad creada.</returns>
        Task<RequestResult<Property>> ExecuteAsync(PropertyDto property);
        /// <summary>
        /// Actualiza el precio de una propiedad existente.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad a actualizar.</param>
        /// <param name="newPrice">Nuevo precio para la propiedad.</param>
        /// <returns>Resultado de la operación de actualización.</returns>
        Task<RequestResult<bool>> UpdatePriceAsync(int propertyId, decimal newPrice);
        /// <summary>
        /// Actualiza los datos de una propiedad existente.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad a actualizar.</param>
        /// <param name="updateDto">Objeto con los datos actualizados de la propiedad.</param>
        /// <returns>Resultado de la operación de actualización.</returns>
        Task<RequestResult<bool>> UpdatePropertyAsync(int propertyId, PropertyUpdateDto updateDto);
        /// <summary>
        /// Obtiene una lista de propiedades aplicando filtros.
        /// </summary>
        /// <param name="filter">Filtros para aplicar en la búsqueda de propiedades.</param>
        /// <returns>Lista de propiedades que cumplen con los filtros especificados.</returns>
        Task<IEnumerable<Property>> GetPropertiesAsync(PropertyFilterDto filter);
    }
}