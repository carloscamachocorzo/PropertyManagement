using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ICreatePropertyAppService _createProperty;
        public PropertiesController(ICreatePropertyAppService createProperty)
        {
            _createProperty = createProperty;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(PropertyDto propertyDto)
        { 
            var createdProperty = await _createProperty.ExecuteAsync(propertyDto);
            return CreatedAtAction(nameof(CreateProperty), new { id = createdProperty.IdProperty }, createdProperty);
        }
    }
}
