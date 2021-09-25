using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specification
{
    public abstract class BaseSpecification<T>
    {
        public bool IsSatisfiedBy(T item)
        {
            return ToExpression().Compile().Invoke(item);
        }

        public abstract Expression<Func<T, bool>> ToExpression();


        #region Specification Combination Methods

        public BaseSpecification<T> All()
        {
            return new AllSpecification<T>();
        }
        public BaseSpecification<T> And(BaseSpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public BaseSpecification<T> Or(BaseSpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public BaseSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }


        #endregion

        #region Specification Operators
        public static BaseSpecification<T> operator &(BaseSpecification<T> left, BaseSpecification<T> right) => left.And(right);

        public static BaseSpecification<T> operator |(BaseSpecification<T> left, BaseSpecification<T> right) => left.Or(right);

        public static BaseSpecification<T> operator !(BaseSpecification<T> specification) => specification.Not();
        #endregion

    }


    #region Specification Combination Classes

    internal sealed class AllSpecification<T> : BaseSpecification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return a => true;
        }
    }

    internal sealed class AndSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _left;
        private readonly BaseSpecification<T> _right;

        public AndSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);
            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.AndAlso(leftExpression.Body, invokedExpression), leftExpression.Parameters);
             
        }
    }


    internal sealed class OrSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _left;
        private readonly BaseSpecification<T> _right;

        public OrSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.OrElse(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }

    internal sealed class NotSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _specification;

        public NotSpecification(BaseSpecification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
    #endregion

}
