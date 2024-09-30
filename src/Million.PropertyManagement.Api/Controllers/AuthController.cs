using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Application.Services.Interfaces;

namespace Million.PropertyManagement.Api.Controllers
{
    /// <summary>
    /// Controlador que gestiona las operaciones de autenticación y registro de usuarios.
    /// </summary>    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authService;
        private readonly IUserAppService _userAppService;
        /// <summary>
        /// Constructor de la clase <see cref="AuthController"/>.
        /// </summary>
        /// <param name="authService">Servicio de autenticación para manejar el login de usuarios.</param>
        /// <param name="userAppService">Servicio para la creación de usuarios en el sistema.</param>

        public AuthController(IAuthAppService authService, IUserAppService userAppService)
        {
            _authService = authService;
            _userAppService = userAppService;
        }
        /// <summary>
        /// Realiza la autenticación de un usuario utilizando sus credenciales.
        /// </summary>
        /// <param name="request">Objeto que contiene el nombre de usuario y la contraseña.</param>
        /// <returns>
        /// Un token JWT si la autenticación es exitosa; de lo contrario, un estado HTTP 401 (Unauthorized).
        /// </returns>
        /// <response code="200">Devuelve el token JWT generado si las credenciales son válidas.</response>
        /// <response code="401">Devuelve un error de autenticación si las credenciales no son válidas.</response>

        [HttpPost("login")]
         
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            string token = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(new { Token = token, TokenType = "Bearer" });
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="request">Objeto que contiene los datos del nuevo usuario a registrar.</param>
        /// <returns>
        /// Un estado HTTP 200 (Ok) si el usuario fue registrado con éxito, o un estado HTTP 400 (Bad Request) si hubo un error.
        /// </returns>
        /// <response code="200">Usuario registrado con éxito.</response>
        /// <response code="400">Error en el registro del usuario.</response>
        [HttpPost("register")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Register([FromForm] RegisterUserRequestDto request)
        {
            var result = await _userAppService.CreateUserAsync(request);

            if (!result.IsSuccessful)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}
