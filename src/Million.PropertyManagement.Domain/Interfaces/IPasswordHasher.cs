namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashea una contraseña y genera un hash junto con una sal.
        /// </summary>
        /// <param name="password">La contraseña a hashear.</param>
        /// <returns>
        /// Una tupla que contiene el hash de la contraseña y la sal utilizada,
        /// ambos representados como arreglos de bytes.
        (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password);
    }
}
