using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PeripheralProfile : Profile
    {
        public PeripheralProfile()
        {
            CreateMap<Peripheral, PeripheralDto>().ReverseMap();
            CreateMap<Peripheral, PeripheralDtoForCreate>().ReverseMap();
            CreateMap<Peripheral, PeripheralDtoForUpdate>().ReverseMap();
        }
    }
}
