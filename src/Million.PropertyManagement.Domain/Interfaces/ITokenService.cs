namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Genera un token JWT para el usuario especificado.
        /// </summary>
        /// <param name="username">Nombre de usuario para el cual se generará el token.</param>
        /// <returns>Un token JWT en forma de cadena.</returns>
        string GenerateToken(string username);
    }
}
