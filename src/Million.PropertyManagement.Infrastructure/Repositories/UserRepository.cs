using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio que maneja las operaciones relacionadas con los usuarios en la base de datos.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly PropertyManagementContext _dbContext;
        /// <summary>
        /// Constructor de la clase <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos utilizado para interactuar con la tabla de usuarios.</param>

        public UserRepository(PropertyManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users? GetUserByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Username == username);
        }
        public async Task AddAsync(Users user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
