using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public partial class CasaProfile : Profile
    {
        public CasaProfile()
        {
            CreateMap<Casa, CasaDto>().ReverseMap();
            CreateMap<Casa, CasaDtoForCreate>().ReverseMap();
            CreateMap<Casa, CasaDtoForUpdate>().ReverseMap();
        }
    }
}
