using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Repository<TEntity>() where TEntity : Entity;
        Task<int> CommitAsync();
        Task RollbackAsync();
        void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class;

        EntityEntry GetEntry<TEntity>(TEntity item) where TEntity: class;

        ChangeTracker ChangeTracker();
    }
}
