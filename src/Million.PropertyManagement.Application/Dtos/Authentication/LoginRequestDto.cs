using System.ComponentModel.DataAnnotations;

namespace Million.PropertyManagement.Application.Dtos.Authentication
{
    /// <summary>
    /// DTO para la solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Obtiene o establece el nombre de usuario del usuario que intenta iniciar sesión.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Username { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario que intenta iniciar sesión.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Password { get; set; }
    }
}
