using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        /// <summary>
        /// Agrega una nueva propiedad a la base de datos.
        /// </summary>
        /// <param name="property">La propiedad a agregar.</param>
        Task AddAsync(Property property);
        /// <summary>
        /// Actualiza una propiedad existente en la base de datos.
        /// </summary>
        /// <param name="property">La propiedad a actualizar.</param>
        Task UpdateAsync(Property property);
        /// <summary>
        /// Obtiene una propiedad por su ID.
        /// </summary>
        /// <param name="id">ID de la propiedad a buscar.</param>
        /// <returns>La propiedad encontrada, o null si no existe.</returns>
        Task<Property?> GetByIdAsync(int id);
        /// <summary>
        /// Obtiene una lista de propiedades aplicando filtros y paginación.
        /// </summary>
        /// <param name="name">Nombre de la propiedad para filtrar.</param>
        /// <param name="minPrice">Precio mínimo para filtrar.</param>
        /// <param name="maxPrice">Precio máximo para filtrar.</param>
        /// <param name="year">Año de la propiedad para filtrar.</param>
        /// <param name="pageSize">Número de propiedades por página.</param>
        /// <param name="pageNumber">Número de la página a obtener.</param>
        /// <returns>Lista de propiedades que cumplen con los filtros especificados.</returns>
        Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string name, decimal? minPrice, decimal? maxPrice, int? year, int pageSize, int pageNumber);
    }
}
