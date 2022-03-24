using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderDto>()
                //.ForMember(g => g.Peripheral, options => options.MapFrom(f => f.Peripheral))
                .ReverseMap();
        }
    }
}
