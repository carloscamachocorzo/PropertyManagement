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
        private readonly IUserAppService _userAppService;

        public AuthController(IAuthAppService authService, IUserAppService userAppService)
        {
            _authService = authService;
            _userAppService = userAppService;
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
