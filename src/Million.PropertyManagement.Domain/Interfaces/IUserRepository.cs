using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Users GetUserByUsername(string username);
    }
}
