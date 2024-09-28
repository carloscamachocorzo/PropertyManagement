using AutoMapper;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services
{
    public class CreatePropertyAppService : ICreatePropertyAppService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public CreatePropertyAppService(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Property> ExecuteAsync(PropertyDto propertyDto)
        {
            try
            {
                // Lógica para validar los datos de la propiedad
                //property.CreatedAt = DateTime.Now;
                Property property = _mapper.Map<Property>(propertyDto);
                await _propertyRepository.AddAsync(property);
                return property;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
