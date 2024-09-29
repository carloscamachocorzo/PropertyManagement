using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Application.Services.Interfaces;

namespace Million.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authService;

        public AuthController(IAuthAppService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var token = _authService.Authenticate(request.Username, request.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciales inválidas");
            }
        }
    }
}
