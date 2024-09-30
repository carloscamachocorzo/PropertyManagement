using AutoMapper;
using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Dtos.Property;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;
using System.Diagnostics;

namespace Million.PropertyManagement.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar propiedades.
    /// </summary>
    public class PropertyAppService : IPropertyAppService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "CreatePropertyAppService";
        private readonly ILogger<PropertyAppService> _logger;

        #region Builder
        
        /// <summary>
        /// Inicializa una nueva instancia del servicio de propiedades.
        /// </summary>
        /// <param name="propertyRepository">Repositorio para gestionar propiedades.</param>
        /// <param name="mapper">Instancia de mapeo para convertir entre DTOs y entidades.</param>
        /// <param name="logger">Logger para registrar información y errores.</param>

        public PropertyAppService(IPropertyRepository propertyRepository, IMapper mapper,
             ILogger<PropertyAppService> logger)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos publicos        
        public async Task<RequestResult<Property>> ExecuteAsync(PropertyDto propertyDto)
        {
            try
            {
                // Lógica para validar los datos de la propiedad                
                Property property = _mapper.Map<Property>(propertyDto);
                await _propertyRepository.AddAsync(property);
                // Devuelve una respuesta exitosa
                return RequestResult<Property>.CreateSuccessful(property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<Property>.CreateError($"Error inesperado: {ex.Message}");
            }
        }
        

        public async Task<RequestResult<bool>> UpdatePriceAsync(int propertyId, decimal newPrice)
        {
            try
            {
                // Paso 1: Validar que el precio no sea negativo
                if (newPrice<=0)
                {
                    return new RequestResult<bool> { IsSuccessful = false, IsError = true, Messages = new string[] { $"el precio  [{newPrice}] debe ser mayor de cero" } };
                }

                // Paso 2: Verificar si la propiedad existe en la base de datos
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null)
                {
                    return new RequestResult<bool> { IsSuccessful = false, IsError = true, Messages = new string[] { $"No se encontro el id [{propertyId}] de la propiedad para actualizar el precio" } };
                }
                property.Price = newPrice;
                await _propertyRepository.UpdateAsync(property);
                return new RequestResult<bool> { IsSuccessful = true, Messages = new string[] { "precio actualizado correctamente" } };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<bool>.CreateError($"Error inesperado: {ex.Message}");
            }
        }
        

        public async Task<RequestResult<bool>> UpdatePropertyAsync(int propertyId, PropertyUpdateDto updateDto)
        {
            try
            {
                // Paso 1: Verificar si la propiedad existe en la base de datos
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null)
                {
                    return new RequestResult<bool> { IsSuccessful = false, IsError = true, Messages = new string[] { $"No se encontro el id [{propertyId}] de la propiedad para actualizar" } };
                }
                // Paso 2: Actualizar los campos de la propiedad
                UpdatePropertyFields(property, updateDto);

                await _propertyRepository.UpdateAsync(property);
                return new RequestResult<bool> { IsSuccessful = true, Messages = new string[] { "propiedad actualizada correctamente" } };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<bool>.CreateError($"Error inesperado: {ex.Message}");
            }
        }
       

        public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync(PropertyFilterDto filter)
        {
            // Registrar log los filtros aplicados
            _logger.LogInformation("Filtros aplicados: Nombre={Name}, Precio Mínimo={MinPrice}, Precio Máximo={MaxPrice}, Año={Year}, Tamaño de página={PageSize}, Número de página={PageNumber}",
                filter.Name, filter.MinPrice, filter.MaxPrice, filter.Year, filter.PageSize, filter.PageNumber);

            var properties = await _propertyRepository.GetPropertiesWithFiltersAsync(
            filter.Name, filter.MinPrice, filter.MaxPrice, filter.Year, filter.PageSize, filter.PageNumber);

            // Proyectar entidades a DTO
            var propertyDtos = properties.Select(p => new PropertyDto
            {
                Name = p.Name,
                Address = p.Address,
                Price = p.Price ?? 0,
                CodeInternal = p.CodeInternal,
                Year = p.Year ?? 0
            });

            return propertyDtos;
        }
        #endregion

        #region Metodos privados

        private void UpdatePropertyFields(Property property, PropertyUpdateDto updateDto)
        {
            property.Name = updateDto.Name ?? property.Name;
            property.Address = updateDto.Address ?? property.Address;
            property.Price = updateDto.Price ?? property.Price;
            property.CodeInternal = updateDto.CodeInternal ?? property.CodeInternal;
            property.Year = updateDto.Year ?? property.Year;
        }
        #endregion
    }
}
