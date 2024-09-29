using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task<Property?> GetByIdAsync(int id);
        Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string city, string state, decimal? minPrice, decimal? maxPrice);
    }
}
