using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        /// <summary>
        /// Agrega una imagen de propiedad a la base de datos.
        /// </summary>
        /// <param name="propertyImage">La imagen de la propiedad a agregar.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// </returns>
        Task AddAsync(PropertyImage propertyImage);

        /// <summary>
        /// Obtiene todas las imágenes asociadas a una propiedad específica.
        /// </summary>
        /// <param name="propertyId">El ID de la propiedad para la cual se desean obtener las imágenes.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona, 
        /// con un resultado que contiene una colección de imágenes de propiedades.
        /// </returns>
        Task<IEnumerable<PropertyImage>> GetImagesByPropertyId(int propertyId);
    }
}
