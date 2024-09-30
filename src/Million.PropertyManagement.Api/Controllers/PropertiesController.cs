using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.Dtos.Property;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;

namespace Million.PropertyManagement.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar propiedades.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyAppService _propertyAppService;
        private readonly IPropertyImageAppService _propertyImageAppService;
        /// <summary>
        /// Inicializa una nueva instancia del controlador de propiedades.
        /// </summary>
        /// <param name="createProperty">Servicio para gestionar propiedades.</param>
        /// <param name="propertyImageAppService">Servicio para gestionar imágenes de propiedades.</param>

        public PropertiesController(IPropertyAppService propertyAppService, IPropertyImageAppService propertyImageAppService)
        {
            _propertyAppService = propertyAppService;
            _propertyImageAppService = propertyImageAppService;

        }
        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>
        /// <param name="propertyDto">Objeto con los datos de la propiedad a crear.</param>
        /// <returns>El resultado de la creación, incluyendo el ID de la propiedad creada en caso de éxito.</returns>
        /// <response code="201">Si la propiedad fue creada exitosamente.</response>
        /// <response code="400">Si ocurrió un error controlado o no controlado durante la creación.</response>

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDto propertyDto)
        {
            var result = await _propertyAppService.ExecuteAsync(propertyDto);

            if (result.IsSuccessful)
            {
                return CreatedAtAction(nameof(CreateProperty), new { id = result.Result }, result);
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

        /// <summary>
        /// Agrega una imagen a una propiedad existente.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad a la que se agregará la imagen.</param>
        /// <param name="imageFile">Archivo de imagen a agregar.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si la imagen fue agregada exitosamente.</response>
        /// <response code="400">Si el archivo de imagen es inválido o si ocurrió un error al agregarla.</response>

        [HttpPost]
        [Route(nameof(AddPropertyImage))]
        [Authorize]
        public async Task<ActionResult<RequestResult<bool>>> AddPropertyImage(int propertyId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0 || !imageFile.ContentType.StartsWith("image/"))
            {
                // Retornar error si el archivo es nulo o vacío en un RequestResult
                var errorResult = RequestResult<bool>.CreateError("El archivo de imagen es inválido o no es una imagen.");
                return BadRequest(errorResult);
            }

            // Intentar agregar la imagen a la propiedad
            var result = await _propertyImageAppService.AddImageToPropertyAsync(propertyId, imageFile.FileName);

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
            return BadRequest(RequestResult<bool>.CreateError("Ocurrió un error al agregar la imagen."));
        }

        /// <summary>
        /// Actualiza el precio de una propiedad.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad a actualizar.</param>
        /// <param name="newPrice">Nuevo precio para la propiedad.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si el precio fue actualizado exitosamente.</response>
        /// <response code="400">Si ocurrió un error durante la actualización del precio.</response>

        [Authorize]
        [HttpPatch("{propertyId}/price")]
        public async Task<IActionResult> UpdatePrice(int propertyId, decimal newPrice)
        {
            var result = await _propertyAppService.UpdatePriceAsync(propertyId, newPrice);

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
            return BadRequest(RequestResult<bool>.CreateError("Ocurrió un error al actualizar el precio."));
        }


        /// <summary>
        /// Actualiza los datos de una propiedad existente.
        /// </summary>
        /// <param name="propertyId">ID de la propiedad a actualizar.</param>
        /// <param name="updateDto">Objeto con los datos actualizados de la propiedad.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si la propiedad fue actualizada exitosamente.</response>
        /// <response code="400">Si ocurrió un error durante la actualización de la propiedad.</response>

        [HttpPut("{propertyId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProperty(int propertyId, PropertyUpdateDto updateDto)
        {
            var result = await _propertyAppService.UpdatePropertyAsync(propertyId, updateDto);

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
        /// <summary>
        /// Obtiene una lista de propiedades filtradas.
        /// </summary>
        /// <param name="filter">Filtros para aplicar en la búsqueda de propiedades.</param>
        /// <returns>Una lista de propiedades que cumplen con los filtros especificados.</returns>
        /// <response code="200">Si las propiedades fueron obtenidas exitosamente.</response>
        /// <response code="404">Si no se encontraron propiedades que coincidan con los filtros aplicados.</response>


        [HttpGet]
        [Route(nameof(GetPropertiesWithFilters))]
        [Authorize]
        public async Task<IActionResult> GetPropertiesWithFilters([FromQuery] PropertyFilterDto filter)
        {
            var properties = await _propertyAppService.GetPropertiesAsync(filter);

            if (!properties.Any()) // Si no se encuentran propiedades
            {
                return NotFound(new Dictionary<string, string> { { "message", "No se encontraron propiedades que coincidan con los filtros aplicados." } });
            }

            return Ok(properties); // Devolver las propiedades encontradas
        }
    }
}
