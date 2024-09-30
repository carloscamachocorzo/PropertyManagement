using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para gestionar propiedades en la base de datos.
    /// </summary>
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyManagementContext _context;
        /// <summary>
        /// Inicializa una nueva instancia del repositorio de propiedades.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
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

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Property.FindAsync(id);
        }

        
        public async Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string name, decimal? minPrice, decimal? maxPrice, int? year, int pageSize, int pageNumber)
        {
            var query = _context.Property.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (year.HasValue)
                query = query.Where(p => p.Year == year);
            // Aplicar paginación
            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);
            return await query.AsNoTracking().ToListAsync();
        }
        
    }
}
