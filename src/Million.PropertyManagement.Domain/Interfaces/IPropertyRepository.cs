using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task<Property?> GetByIdAsync(int id);        
        Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string name, decimal? minPrice, decimal? maxPrice, int? year, int pageSize, int pageNumber);
    }
}
