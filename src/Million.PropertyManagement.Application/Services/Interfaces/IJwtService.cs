namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
