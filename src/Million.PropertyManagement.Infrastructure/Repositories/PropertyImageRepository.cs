using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly PropertyManagementContext _dbContext;

        public PropertyImageRepository(PropertyManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PropertyImage propertyImage)
        {
            await _dbContext.PropertyImage.AddAsync(propertyImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyImage>> GetImagesByPropertyId(int propertyId)
        {
            return await _dbContext.PropertyImage
                                   .Where(p => p.IdProperty == propertyId)
                                   .ToListAsync();
        }
    }
}
