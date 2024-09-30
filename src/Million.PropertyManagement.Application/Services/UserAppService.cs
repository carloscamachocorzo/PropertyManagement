using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        #region Builder       
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserAppService"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio para la gestión de usuarios.</param>
        /// <param name="passwordHasher">Servicio para hashear contraseñas.</param>
        public UserAppService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

        }
        #endregion

        #region Metodos publicos       
        public async Task<RequestResult<bool>> CreateUserAsync(RegisterUserRequestDto request)
        {
            try
            {
                // Validar si el usuario ya existe
                var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
                if (existingUser != null)
                {
                    return RequestResult<bool>.CreateError("El usuario ya existe.");
                }
                if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                {
                    return RequestResult<bool>.CreateError("La contraseña debe tener al menos 6 caracteres.");
                }

                if (!IsValidEmail(request.Email))
                {
                    return RequestResult<bool>.CreateError("El correo electrónico proporcionado no es válido.");
                }

                // Hashear la contraseña
                var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.Password);

                // Crear la entidad usuario
                var user = new Users
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = request.Email,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow

                };

                // Guardar el usuario
                await _userRepository.AddAsync(user);

                // Retornar el resultado exitoso
                return RequestResult<bool>.CreateSuccessful(true, new string[] { "Usuario creado correctamente." });
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError($"Error inesperado: {ex.Message}");
            }

        }
        #endregion
        #region Metodos privados
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
