using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>()
                .ForMember(g => g.Gateway, options => options.MapFrom(f => f.Gateway))
                .ReverseMap();
        }
    }
}
