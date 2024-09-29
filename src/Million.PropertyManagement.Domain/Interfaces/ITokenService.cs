namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
