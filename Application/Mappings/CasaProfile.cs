using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class CasaProfile:Profile
    {
        public CasaProfile()
        {
            CreateMap<Casa, CasaDto>().ReverseMap();
            CreateMap<Casa, CasaDtoForCreate>().ReverseMap();
            CreateMap<Casa, CasaDtoForUpdate>().ReverseMap();
        }
    }
}
