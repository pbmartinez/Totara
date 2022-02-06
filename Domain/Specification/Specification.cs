using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Specification
{
    /*
     * Taken from course Specification Pattern in C# by Vladimir Khorikov
     * 
     * Variation: In order to convert specifications from dtos to entity, 
     * the expression must be seteable in the constructor (or by a public field) so new specifications of diferent type
     * can be initialized during the mapping. 
     * Otherwise specifications could be only applied in repository layer
     * 
     */
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T>
    {        
        /// <summary>
        /// It validates objects in memory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(T item)
        {
            return ToExpression().Compile().Invoke(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<T, bool>> ToExpression() => new AllSpecification<T>().ToExpression();

        

        #region Specification Combination Methods
        /// <summary>
        /// Basic specification definition. It just returns true for any object.
        /// </summary>
        /// <returns></returns>
        public Specification<T> All = new AllSpecification<T>();
        
        /// <summary>
        /// Combines the current specification with the specification passed as argument in an && operation
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if(specification==All)
                return this;
            return new AndSpecification<T>(this, specification);
        }
        /// <summary>
        /// Combines the current specification with the specification passed as argument in an || operation
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;
            return new OrSpecification<T>(this, specification);
        }
        /// <summary>
        /// Returns the opposite of the current specification
        /// </summary>
        /// <returns></returns>
        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        
        #endregion

        #region Specification Operators
        /// <summary>
        /// Overriden operator to perform && operation
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Specification<T> operator &(Specification<T> left, Specification<T> right) => left.And(right);
        /// <summary>
        /// Overriden operator to perform || operation
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Specification<T> operator |(Specification<T> left, Specification<T> right) => left.Or(right);
        /// <summary>
        /// Overriden operator to perform the negation operation
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static Specification<T> operator !(Specification<T> specification) => specification.Not();
        #endregion

    }


    #region Specification Combination Classes

    internal sealed class AllSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return a => true;
        }
    }

    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
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


    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
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

    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
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
