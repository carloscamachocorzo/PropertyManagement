using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Million.PropertyManagement.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Million.PropertyManagement.Infrastructure.Security
{
    /// <summary>
    /// Servicio que gestiona la generación de tokens JWT.
    /// </summary>
    public class JwtService : ITokenService
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Constructor de la clase <see cref="JwtService"/>.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación utilizada para obtener los parámetros del token JWT.</param>

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
