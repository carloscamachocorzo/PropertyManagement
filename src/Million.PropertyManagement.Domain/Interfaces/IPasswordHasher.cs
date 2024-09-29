namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password);
    }
}
