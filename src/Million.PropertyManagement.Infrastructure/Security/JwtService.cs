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
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        private readonly int expiresInMinutes;

        private readonly IConfiguration _configuration;
        /// <summary>
        /// Constructor de la clase <see cref="JwtService"/>.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación utilizada para obtener los parámetros del token JWT.</param>

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            key = configuration["JwtSettings:SecretKey"];
            issuer = configuration["JwtSettings:Issuer"];
            audience = configuration["JwtSettings:Audience"];
            expiresInMinutes = int.Parse(configuration["JwtSettings:ExpiryInMinutes"]);
        }

        public string GenerateToken(string username)
        { 

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
