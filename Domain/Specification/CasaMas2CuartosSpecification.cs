using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Domain.Specification.BaseSpecification2;

namespace Domain.Specification
{
    public class CasaMas2CuartosSpecification : BaseSpecifcation2<Casa>
    {
        public CasaMas2CuartosSpecification(int cantCuartos) : base(a => a.CantidadCuartos >= cantCuartos)
        {

        }
    }
}
