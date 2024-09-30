namespace Million.PropertyManagement.Application.Dtos.Authentication
{
    /// <summary>
    /// DTO para la solicitud de registro de un nuevo usuario.
    /// </summary>
    public class RegisterUserRequestDto
    {
        /// <summary>
        /// Obtiene o establece el nombre de usuario del nuevo usuario.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del nuevo usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del nuevo usuario.
        /// </summary>
        public string Email { get; set; }
    }

}
