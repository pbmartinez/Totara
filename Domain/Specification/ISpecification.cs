using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }
}
