using Million.PropertyManagement.Domain.Interfaces;

namespace Million.PropertyManagement.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }
    }
}
