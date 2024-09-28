using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyManagementContext _context;

        public PropertyRepository(PropertyManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Property property)
        {
            _context.Property.Add(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Property.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        public Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string city, string state, decimal? minPrice, decimal? maxPrice)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string city, string state, decimal? minPrice, decimal? maxPrice)
        //{
        //    return await _context.Property
        //        .Where(p => (string.IsNullOrEmpty(city) || p.City == city) &&
        //                    (string.IsNullOrEmpty(state) || p.ena == state) &&
        //                    (!minPrice.HasValue || p.Price >= minPrice) &&
        //                    (!maxPrice.HasValue || p.Price <= maxPrice))
        //        .ToListAsync();
        //}
    }
}
