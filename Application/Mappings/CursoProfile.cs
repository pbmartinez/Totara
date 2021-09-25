using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CursoProfile : Profile
    {
        public CursoProfile()
        {
            CreateMap<Curso, CursoDto>().ReverseMap();
            CreateMap<Curso, CursoDtoForCreate>().ReverseMap();
            CreateMap<Curso, CursoDtoForUpdate>().ReverseMap();
        }
    }
}
