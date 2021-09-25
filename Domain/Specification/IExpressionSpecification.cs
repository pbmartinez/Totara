using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specification
{
    public interface IExpressionSpecification<TEntity> : ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
    }
}
