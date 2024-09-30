using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task AddAsync(PropertyImage propertyImage);  // Agregar una imagen de propiedad
        Task<IEnumerable<PropertyImage>> GetImagesByPropertyId(int propertyId);  // Obtener imágenes de una propiedad
    }
}
