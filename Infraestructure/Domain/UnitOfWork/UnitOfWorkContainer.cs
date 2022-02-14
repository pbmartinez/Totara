using Domain.Entities;
using Domain.Infraestructure;
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
    public class UnitOfWorkContainer : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private const string STRING_CONNECTION = "DefaultConnection";
        public UnitOfWorkContainer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>().HasKey(e => new { e.EstudianteId, e.CursoId });

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Matriculas)
                .WithOne(a => a.Estudiante).IsRequired().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.Matriculas)
                .WithOne(a => a.Curso).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(STRING_CONNECTION));
            base.OnConfiguring(optionsBuilder);
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
        
        public IQueryable<TEntity> GetQueryable<TEntity>(List<Expression<Func<TEntity, object>>> includes = null, Expression<Func<TEntity,bool>> predicate = null, Dictionary<string,bool> order = null, int pageSize = 0, int pageGo = 0) where TEntity : class
        {
            IQueryable<TEntity> items = Set<TEntity>();
            if (includes != null && includes.Any())
                includes.ForEach(a => items = items.Include(a));

            if(predicate!= null)
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
            
            return  items;
        }

        public virtual DbSet<Persona> Persona { get; set; } 
        public virtual DbSet<Casa> Casa { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Escuela> Escuela { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Matricula> Matricula { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
    }
}
