using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;
using System.Diagnostics;

namespace Million.PropertyManagement.Application.Services
{
    public class PropertyImageAppService : IPropertyImageAppService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;
        private string className = new StackFrame().GetMethod()?.ReflectedType?.Name ?? "CreatePropertyAppService";
        private readonly ILogger<PropertyImageAppService> _logger;

        #region Builder        
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PropertyImageAppService"/>.
        /// </summary>
        /// <param name="propertyImageRepository">Repositorio para la gestión de imágenes de propiedades.</param>
        /// <param name="propertyRepository">Repositorio para la gestión de propiedades.</param>
        /// <param name="logger">Instancia del logger para registrar información de la aplicación.</param>
        public PropertyImageAppService(IPropertyImageRepository propertyImageRepository,
            IPropertyRepository propertyRepository,
            ILogger<PropertyImageAppService> logger)
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;
            _logger = logger;
        }
        #endregion

        #region Metodos publicos        
        public async Task<RequestResult<bool>> AddImageToPropertyAsync(int propertyId, string fileName)
        {
            try
            {
                // Paso 1: Verificar si la propiedad existe en la base de datos
                var property = await _propertyRepository.GetByIdAsync(propertyId);
                if (property == null)
                {                    
                    return RequestResult<bool>.CreateUnsuccessful(new string[] { $"No se encontró la propiedad con id [{propertyId}] para agregar la imagen." });
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

                return RequestResult<bool>.CreateSuccessful(true, new string[] { "imagen guardada correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, className, (new StackFrame().GetMethod())?.Name + (new StackFrame().GetFileLineNumber()));
                return RequestResult<bool>.CreateError($"Error inesperado: {ex.Message}");
            }
        }
        #endregion
    }
}
