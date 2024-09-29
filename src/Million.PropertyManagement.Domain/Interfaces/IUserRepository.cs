using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Obtiene un usuario de la base de datos por su nombre de usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario del usuario a buscar.</param>
        /// <returns>
        /// El usuario correspondiente al nombre de usuario proporcionado, o <c>null</c> si no se encuentra.
        /// </returns>
        Users? GetUserByUsername(string username);
        /// <summary>
        /// Agrega un nuevo usuario a la base de datos de manera asíncrona.
        /// </summary>
        /// <param name="user">Entidad de usuario que se va a agregar a la base de datos.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task AddAsync(Users user);
    }
}
