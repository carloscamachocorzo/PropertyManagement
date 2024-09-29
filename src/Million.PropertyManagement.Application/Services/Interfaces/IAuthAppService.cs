namespace Million.PropertyManagement.Application.Services.Interfaces
{
    public interface IAuthAppService
    {
        /// <summary>
        /// Autentica un usuario validando su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="username">Nombre de usuario del usuario que intenta autenticarse.</param>
        /// <param name="password">Contraseña proporcionada por el usuario.</param>
        /// <returns>
        /// El token JWT generado si las credenciales son correctas; de lo contrario, una cadena vacía.
        /// </returns>  
        string Authenticate(string username, string password);
    }
}
