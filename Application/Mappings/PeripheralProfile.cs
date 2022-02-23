using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PeripheralProfile : Profile
    {
        public PeripheralProfile()
        {
            CreateMap<Peripheral, PeripheralDto>()
                .ForMember(g => g.Gateway, options => options.MapFrom(f => f.Gateway))
                .ReverseMap();
        }
    }
}
