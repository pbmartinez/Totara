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
        DbSet<TEntity> Repository<TEntity>() where TEntity : Entity;
        Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>(Expression<Func<TEntity, object>> Includes) 
            where TEntity : class;
        Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>(List<Expression<Func<TEntity, object>>> Includes) 
            where TEntity : class;
        Task<int> CommitAsync();
        Task RollbackAsync();
        void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class;
        EntityEntry GetEntry<TEntity>(TEntity item) where TEntity: class;
        ChangeTracker ChangeTracker();
    }
}
