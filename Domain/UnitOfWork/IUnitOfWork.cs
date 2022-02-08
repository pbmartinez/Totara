using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        /// Access to any repository in a queryable form in a manner that can specified the related entities that shoud be Included
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Includes"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>(List<Expression<Func<TEntity, object>>> Includes = null, Expression<Func<TEntity,bool>> predicate = null) 
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
        
        EntityEntry GetEntry<TEntity>(TEntity item) where TEntity: class;
        ChangeTracker ChangeTracker();
    }
}
