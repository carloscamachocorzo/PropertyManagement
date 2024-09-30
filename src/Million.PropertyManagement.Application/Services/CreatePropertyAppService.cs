using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;
using System.Diagnostics;

namespace Million.PropertyManagement.Application.Services
{
    public class CreatePropertyAppService : ICreatePropertyAppService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "CreatePropertyAppService";
        private readonly ILogger<CreatePropertyAppService> _logger;
        public CreatePropertyAppService(IPropertyRepository propertyRepository, IMapper mapper,
             ILogger<CreatePropertyAppService> logger)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _logger = logger;
        }

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
                // Paso 1: Verificar si la propiedad existe en la base de datos
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
    }
}
