using AutoMapper;
using Million.PropertyManagement.Application.Dtos.Property;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Automapper
{
    public sealed class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile() : base()
        {            
            CreateMap<Property, PropertyDto>().ReverseMap();
        }
    }
}
