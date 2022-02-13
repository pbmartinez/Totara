using Application.Dtos;
using AutoMapper;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Application.Specification
{
    public class CasaMas2CuartosSpecification : Specification<CasaDto>
    {
        public CasaMas2CuartosSpecification(IMapper mapper) : base(mapper)
        {
        }

        public override Expression<Func<CasaDto, bool>> ToExpression()
        {
            return casa => casa.CantidadCuartos > 2;  
        }
    }
}
