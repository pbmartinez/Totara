using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            CreateMap<Gateway, GatewayDto>()
                .ForMember(g => g.Peripherals, options => options.MapFrom(f => f.Peripherals))
                .ReverseMap();

            CreateMap<Gateway, GatewayDtoCommon>()
                .ForMember(g => g.Peripherals, options => options.MapFrom(f => f.Peripherals))
                .ReverseMap();

            CreateMap<Gateway, GatewayDtoForCreate>()
                .ForMember(g => g.Peripherals, options => options.MapFrom(f => f.Peripherals))
                .ReverseMap();
            
            CreateMap<Gateway, GatewayDtoForUpdate>()
                .ForMember(g => g.Peripherals, options => options.MapFrom(f => f.Peripherals))
                .ReverseMap();
        }
    }
}
