using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services;
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
        public async Task<ActionResult<RequestResult<Property>>> CreateProperty([FromBody] PropertyDto propertyDto)
        {
            // Llamada al servicio que devuelve RequestResult<Property>
            var result = await _createPropertyAppService.ExecuteAsync(propertyDto);

            // Verifica si la operación fue exitosa
            if (result.IsSuccessful)
            {
                // Devuelve un 201 (Created) con el RequestResult y la propiedad creada
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
