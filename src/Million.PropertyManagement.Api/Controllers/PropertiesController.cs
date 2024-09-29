using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ICreatePropertyAppService _createPropertyAppService;
        public PropertiesController(ICreatePropertyAppService createProperty)
        {
            _createPropertyAppService = createProperty;
        }

        [HttpPost]
        public async Task<ActionResult<RequestResult<CreatePropertyResponseDto>>> CreateProperty([FromBody] PropertyDto propertyDto)
        {
            var result = await _createPropertyAppService.ExecuteAsync(propertyDto);

            if (result.IsSuccessful)
            {
                return CreatedAtAction(nameof(CreateProperty), new { id = result.Result.IdProperty }, result);
            }

            // Verifica si fue un error controlado
            if (result.IsError)
            {
                // Devuelve un 400 (BadRequest) con el RequestResult que contiene el mensaje de error
                return BadRequest(result);
            }

            // Si no fue exitoso ni un error controlado, devuelve un 400 (BadRequest) con mensajes adicionales
            return BadRequest(result);
        }
    }
}
