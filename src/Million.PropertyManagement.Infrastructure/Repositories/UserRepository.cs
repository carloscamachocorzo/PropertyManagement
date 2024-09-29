using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure.DataAccess.Contexts;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PropertyManagementContext _dbContext;

        public UserRepository(PropertyManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users GetUserByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
