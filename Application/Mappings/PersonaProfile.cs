using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public partial class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<Persona, PersonaDto>().ReverseMap();
            CreateMap<Persona, PersonaDtoForCreate>().ReverseMap();
            CreateMap<Persona, PersonaDtoForUpdate>().ReverseMap();
        }
    }
}
