using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using System.Diagnostics;
using System.Text;

namespace Million.PropertyManagement.Application.Services
{
    /// <summary>
    /// Servicio de autenticación que maneja la validación de credenciales y la generación de tokens JWT.
    /// </summary>
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthAppService> _logger;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "AuthAppService";

        #region Builder
        /// <summary>
        /// Constructor de la clase <see cref="AuthAppService"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios utilizado para consultar datos de usuario.</param>
        /// <param name="tokenService">Servicio utilizado para generar el token JWT.</param>
        /// <param name="logger">Instancia para registrar logs.</param>
        public AuthAppService(IUserRepository userRepository, ITokenService tokenService,
            ILogger<AuthAppService> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }
        #endregion

        #region Metodos Publicos
        public string Authenticate(string username, string password)
        {
            try
            {
                // 1. Validar las credenciales contra la capa de dominio
                var user = _userRepository.GetUserByUsername(username);
                if (user == null)
                    return string.Empty;
                // 2. Generar el token (por ejemplo, usando JWT)
                bool isValid = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
                if (isValid)
                {
                    return _tokenService.GenerateToken(user.Username);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, new StackFrame().GetMethod()?.Name ?? "UnknownMethod" + (new StackFrame().GetFileLineNumber()));
                return string.Empty;
            }
        }
        #endregion

        #region Metodos privados
        /// <summary>
        /// Verifica si la contraseña proporcionada coincide con el hash almacenado.
        /// </summary>
        /// <param name="password">Contraseña proporcionada por el usuario.</param>
        /// <param name="storedHash">Hash de la contraseña almacenada en la base de datos.</param>
        /// <param name="storedSalt">Salt utilizado para generar el hash de la contraseña.</param>
        /// <returns><c>true</c> si la contraseña es correcta; de lo contrario, <c>false</c>.</returns>

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
        #endregion

    }
}
