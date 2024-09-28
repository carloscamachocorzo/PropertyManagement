using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;
using System.Diagnostics;

namespace Million.PropertyManagement.Application.Services
{
    public class CreatePropertyAppService : ICreatePropertyAppService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string className = new StackFrame().GetMethod().ReflectedType.Name;
        private readonly ILogger<CreatePropertyAppService> _logger;
        public CreatePropertyAppService(IPropertyRepository propertyRepository, IMapper mapper,
            IConfiguration configuration, ILogger<CreatePropertyAppService> logger)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger= logger;
        }

        public async Task<Property> ExecuteAsync(PropertyDto propertyDto)
        {
            try
            {
                // Lógica para validar los datos de la propiedad                
                Property property = _mapper.Map<Property>(propertyDto);
                await _propertyRepository.AddAsync(property);
                return property;
            }
            catch (Exception ex)
            {

                if (Convert.ToBoolean(_configuration.GetSection("logger").Value))
                {
                    //Logger.WriteException(ex);
                    //Logger.WriteInfoMessage(ex.Message);
                }
                _logger.LogError(ex, className, (new StackFrame().GetMethod()).Name + (new StackFrame().GetFileLineNumber()));
                return null;
            }
            
        }
    }
}
