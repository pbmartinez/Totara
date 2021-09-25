﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specification
{
    public class BaseSpecification
    {
        public class BaseSpecifcation<TEntity> : IExpressionSpecification<TEntity>
        {
            public BaseSpecifcation(Expression<Func<TEntity, bool>> criteria)
            {
                Criteria = criteria;
            }
            public BaseSpecifcation(Expression<Func<TEntity, bool>> criteria,
                List<Expression<Func<TEntity, object>>> includes,
                Expression<Func<TEntity, object>> orderBy )
            {
                Criteria = criteria;
                Includes = includes;
                OrderBy = orderBy;

            }

            

            public Expression<Func<TEntity, bool>> Criteria { get; }
            public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
            public Expression<Func<TEntity, object>> OrderBy { get; private set; }
            public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

            public bool IsSatisfied(TEntity item)
            {
                throw new NotImplementedException();
            }

            protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            {
                Includes.Add(includeExpression);
            }

            protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            {
                OrderBy = orderByExpression;
            }

            protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
            {
                OrderByDescending = orderByDescExpression;
            }
        }
    }
}
