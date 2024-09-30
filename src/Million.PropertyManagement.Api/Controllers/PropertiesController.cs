using Microsoft.AspNetCore.Authorization;
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
        private readonly IPropertyImageAppService _propertyImageAppService;
        public PropertiesController(ICreatePropertyAppService createProperty, IPropertyImageAppService propertyImageAppService)
        {
            _createPropertyAppService = createProperty;
            _propertyImageAppService = propertyImageAppService;

        }

        [HttpPost]
        [Authorize]   
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

        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RequestResult<bool>>> AddPropertyImage(int propertyId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                // Retornar error si el archivo es nulo o vacío en un RequestResult
                var errorResult = RequestResult<bool>.CreateError("El archivo de imagen es inválido.");
                return BadRequest(errorResult);
            }

            // Intentar agregar la imagen a la propiedad
            var result = await _propertyImageAppService.AddImageToPropertyAsync(propertyId, imageFile.FileName);

            if (result.IsSuccessful)
            {
                // Retornar éxito con la confirmación de la creación de la imagen
                return Ok(RequestResult<bool>.CreateSuccessful(true));
            }

            if (result.IsError)
            {
                // Retornar error controlado
                return BadRequest(result);
            }

            // Retornar un 400 si hubo algún otro error
            return BadRequest(RequestResult<bool>.CreateError("Ocurrió un error al agregar la imagen."));
        }
        [HttpPut("{propertyId}/price")]
        [Authorize]
        public async Task<IActionResult> UpdatePrice(int propertyId, decimal newPrice)
        {
            var result = await _createPropertyAppService.UpdatePriceAsync(propertyId, newPrice);

            if (result.IsSuccessful)
            {
                // Retornar éxito con la confirmación de la creación de la imagen
                return Ok(RequestResult<bool>.CreateSuccessful(true));
            }

            if (result.IsError)
            {
                // Retornar error controlado
                return BadRequest(result);
            }

            // Retornar un 400 si hubo algún otro error
            return BadRequest(RequestResult<bool>.CreateError("Ocurrió un error al actualizar el precio."));
        }

        [HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdateProperty(int propertyId, PropertyUpdateDto updateDto)
        {
            var result = await _createPropertyAppService.UpdatePropertyAsync(propertyId, updateDto);

            if (result.IsSuccessful)
            {
                // Retornar éxito con la confirmación de la creación de la imagen
                return Ok(result);
            }

            if (result.IsError)
            {
                // Retornar error controlado
                return BadRequest(result);
            }

            // Retornar un 400 si hubo algún otro error
            return BadRequest(RequestResult<bool>.CreateError("Ocurrió un error al actualizar la propiedad."));
        }


    }
}
