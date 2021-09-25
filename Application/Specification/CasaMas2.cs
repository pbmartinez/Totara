using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Specification.BaseSpecification2;

namespace Application.Specification
{
    public class CasaMas2Dto : BaseSpecifcation<CasaDto>
    {
        public CasaMas2Dto(int cantCuartos) : base(a => a.CantidadCuartos >= cantCuartos)
        {

        }
    }
}
