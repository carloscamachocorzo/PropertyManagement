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
        public UserAppService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

        }
        public async Task<RequestResult<bool>> CreateUserAsync(RegisterUserRequestDto request)
        {
            // Validar si el usuario ya existe
            var existingUser =  _userRepository.GetUserByUsername(request.Username);
            if (existingUser != null)
            {
                return new RequestResult<bool> { IsSuccessful = false, Messages = new string[] { "usuario ya existe" } };
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
                CreatedAt = DateTime.UtcNow,
                Salt = ""

            };

            // Guardar el usuario
            await _userRepository.AddAsync(user);

            // Retornar el resultado exitoso
            return new RequestResult<bool> { IsSuccessful = true };
        }

    }
}
