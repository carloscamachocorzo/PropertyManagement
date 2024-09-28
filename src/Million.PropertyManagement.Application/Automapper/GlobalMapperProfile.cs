using AutoMapper;
using Million.PropertyManagement.Application.Dtos;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Automapper
{
    public sealed class GlobalMapperProfile : Profile
    {
        public GlobalMapperProfile() : base()
        {
            CreateMap<Property, PropertyDto>();
        }
    }
}
