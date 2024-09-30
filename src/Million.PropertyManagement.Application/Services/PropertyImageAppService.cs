using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;
using Million.PropertyManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.PropertyManagement.Application.Services
{
    public class PropertyImageAppService : IPropertyImageAppService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "CreatePropertyAppService";
        private readonly ILogger<PropertyImageAppService> _logger;
        public PropertyImageAppService(IPropertyImageRepository propertyImageRepository,
            IPropertyRepository propertyRepository,
            ILogger<PropertyImageAppService> logger)
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;
            _logger = logger;
        }

        
        public async Task<RequestResult<bool>> AddImageToPropertyAsync(int propertyId, string fileName)
        {
            try
            {
                // Paso 1: Verificar si la propiedad existe en la base de datos
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null)
                {
                    return new RequestResult<bool> { IsSuccessful = false, Messages = new string[] { "No se encuentra la propiedad para adicionar la imagen" } };
                }

                // Paso 2: Crear un nuevo registro en la tabla PropertyImage
                var propertyImage = new PropertyImage
                {
                    IdProperty = propertyId,    // Asociamos la imagen a la propiedad
                    FileImage = fileName,            // El nombre del archivo o ruta donde se almacenó
                    Enabled = true              // Asumimos que la imagen está habilitada por defecto
                };

                // Paso 3: Guardar el registro de imagen en la base de datos
                // Agregar la imagen al repositorio
                await _propertyImageRepository.AddAsync(propertyImage);

                return new RequestResult<bool> { IsSuccessful = true, Messages = new string[] { "imagen guardada correctamente" } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<bool>.CreateError($"Error inesperado: {ex.Message}");
            }
        }
    }
}
