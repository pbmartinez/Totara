using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static Domain.Specification.BaseSpecification;

namespace Domain.Specification
{
    public class CasaMas2CuartosSpecification : BaseSpecifcation<Casa>
    {
        public CasaMas2CuartosSpecification(int cantCuartos) : base(a => a.CantidadCuartos >= cantCuartos)
        {

        }
    }
}
