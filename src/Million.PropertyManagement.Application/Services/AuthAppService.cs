using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.PropertyManagement.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthAppService> _logger;  // Inyectar ILogger
        private string className = new StackFrame().GetMethod().ReflectedType.Name;
        public AuthAppService(IUserRepository userRepository, ITokenService tokenService,
            ILogger<AuthAppService> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }
        public string Authenticate(string username, string password)
        {
            try
            {
                // 1. Validar las credenciales contra la capa de dominio
                var user = _userRepository.GetUserByUsername(username);
                if (user == null)
                    return string.Empty;
                // 2. Generar el token (por ejemplo, usando JWT)
                bool isValid=VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
                if (isValid)
                {
                    return _tokenService.GenerateToken(user.Username);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod()).Name + (new StackFrame().GetFileLineNumber()));
                return string.Empty;
            }
        }
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
