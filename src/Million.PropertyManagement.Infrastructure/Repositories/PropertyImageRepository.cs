using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly PropertyManagementContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PropertyImageRepository"/>.
        /// </summary>
        /// <param name="dbContext">El contexto de la base de datos que se utilizará para las operaciones de acceso a datos.</param>

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
