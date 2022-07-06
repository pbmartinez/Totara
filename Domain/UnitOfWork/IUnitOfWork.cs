using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
        /// <param name="includes"></param>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageGo"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable<TEntity>(List<string>? includes = null, Expression<Func<TEntity, bool>>? predicate = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0)
            where TEntity : class;

        /// <summary>
        /// It commits all pending changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back all changes during transaction.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the state (Detached, Unchanged, Deleted, Modified, Added) of a given entity 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <param name="state"></param>
        void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class;

        EntityEntry GetEntry<TEntity>(TEntity item) where TEntity : class;
        ChangeTracker ChangeTracker();

        /// <summary>
        /// Executes a query against the database. (i.e reading statement). 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> ExecuteQuery<T>(string query, params object[] parameters) where T : class;

        /// <summary>
        /// Excetutes a command against the database. i.e writing statement.Synchronic version
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        /// <summary>
        /// Executes a command against the database. i.e writing statement. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteCommandAsync(string sqlCommand, CancellationToken cancellationToken = default, params object[] parameters);
    }
}
