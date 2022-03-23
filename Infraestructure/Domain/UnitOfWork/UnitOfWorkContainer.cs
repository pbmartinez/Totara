using Domain.Entities;
using Domain.Utils;
using Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infraestructure.Domain.UnitOfWork
{
    public class UnitOfWorkContainer : BaseDbContext, IUnitOfWork
    {
        public UnitOfWorkContainer(DbContextOptions<UnitOfWorkContainer> options) : base(options)
        {

        }

        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        public DbSet<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>();
        }

        public async Task RollbackAsync()
        {
            await RollbackAsync();
        }

        public void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class
        {
            Entry(item).State = state;
        }

        public EntityEntry GetEntry<TEntity>(TEntity item) where TEntity : class
        {
            return Entry(item);
        }

        ChangeTracker IUnitOfWork.ChangeTracker()
        {
            return ChangeTracker;
        }

        
        public IQueryable<TEntity> GetQueryable<TEntity>(List<string>? includes = null, Expression<Func<TEntity, bool>>? predicate = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0) where TEntity : class
        {
            IQueryable<TEntity> items = Set<TEntity>();
            if (includes != null && includes.Any())
                includes.Where(i => !string.IsNullOrEmpty(i) && !string.IsNullOrWhiteSpace(i)).ToList()
                    .ForEach(a => items = items.Include(a));

            if (predicate != null)
                items = items.Where(predicate);

            if (order != null && order.Any())
            {
                order.Where(i => i.Key != null).ToList()
                    .ForEach(s => items = QueryableUtils.CallOrderBy(items, s.Key, s.Value));
                if (pageSize > 0)
                {
                    var skip = pageSize * (pageGo - 1);
                    skip = skip >= 0 ? skip : 0;
                    items = items.Skip(skip);
                }
            }
            if (pageSize > 0)
            {
                items = items.Take(pageSize);
            }

            return items;
        }

        public virtual DbSet<Gateway>? Gateway { get; set; }
        public virtual DbSet<Peripheral>? Peripheral { get; set; }
        public virtual DbSet<Brand>? Brand { get; set; }
    }
}
