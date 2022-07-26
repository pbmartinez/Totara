﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class QueryableUtils
    {
        #region OrderBy

        public static readonly MethodInfo OrderByAscending =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "OrderBy")
                .Single(method => method.GetParameters().Length == 2);

        public static readonly MethodInfo OrderByDescending =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "OrderByDescending")
                .Single(method => method.GetParameters().Length == 2);

        public static IQueryable<TSource> CallOrderBy<TSource>(IQueryable<TSource> source, string propertyName,
            bool ascending)
        {
            var orderMethod = ascending ? OrderByAscending : OrderByDescending;
            var lambda = GenerateSelector<TSource>(propertyName, out var propertyType);

            var genericMethod = orderMethod.MakeGenericMethod(typeof(TSource), propertyType);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }

        private static LambdaExpression GenerateSelector<TEntity>(string propertyName, out Type resultType)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "Entity");
            PropertyInfo property;
            Expression propertyAccess;

            if (propertyName.Contains('.'))
            {
                var childProperties = propertyName.Split('.');
                property = typeof(TEntity).GetProperty(childProperties[0],
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?? throw new NullReferenceException(string.Format(Localization.Resource.Exception_NullFieldOnOrderingEntity, nameof(property), typeof(TEntity)));
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (var i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i],
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                        ?? throw new NullReferenceException(string.Format(Localization.Resource.Exception_NullFieldOnOrderingEntity, nameof(property), typeof(TEntity)));
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(TEntity).GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?? throw new NullReferenceException(string.Format(Localization.Resource.Exception_NullFieldOnOrderingEntity, nameof(property), typeof(TEntity)));
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }
            resultType = property.PropertyType;
            return Expression.Lambda(propertyAccess, parameter);
        }

        #endregion
    }
}
