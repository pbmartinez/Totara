﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Access to any repository.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> Repository<TEntity>() where TEntity : Entity;

        /// <summary>
        /// Access to any repository of type TEntity in a queryable form. 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="includes">Related entities to be loaded</param>
        /// <param name="predicate">The expression that defines the items to be retrieved</param>
        /// <param name="order">Fields to be ordered</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageGo">Target page</param>
        /// <returns></returns>        
        IQueryable<TEntity> GetQueryable<TEntity>(List<Expression<Func<TEntity, object>>> includes = null, Expression<Func<TEntity, bool>> predicate = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
            where TEntity : class;

        /// <summary>
        /// It commits all pending changes
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Rolls back all changes during transaction
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();

        /// <summary>
        /// Sets the state (Detached, Unchanged, Deleted, Modified, Added) of a given entity 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <param name="state"></param>
        void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class;

        EntityEntry GetEntry<TEntity>(TEntity item) where TEntity : class;
        ChangeTracker ChangeTracker();
    }
}
