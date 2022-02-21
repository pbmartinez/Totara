using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            CreateMap<Gateway, GatewayDto>().ReverseMap();
            CreateMap<Gateway, GatewayDtoForCreate>().ReverseMap();
            CreateMap<Gateway, GatewayDtoForUpdate>().ReverseMap();
        }
    }
}
