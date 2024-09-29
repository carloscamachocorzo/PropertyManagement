namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IAuthAppService
    {
        string Authenticate(string username, string password);
    }
}
