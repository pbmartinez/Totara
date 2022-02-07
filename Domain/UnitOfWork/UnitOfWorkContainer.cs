using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
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

        public async Task<IQueryable<TEntity>> GetQueryableAsync<TEntity >(Expression<Func<TEntity, object>> Includes) 
            where TEntity : class
        {
            var items = Set<TEntity>().Include(Includes);
            return await Task.FromResult(items);
        }
        public IQueryable<TEntity> GetQueryable2<TEntity>(Expression<Func<TEntity, TEntity>> Includes) where TEntity : Entity
        {
            var items = Set<TEntity>().Include(Includes);
            return items;
        }

        public async Task<IQueryable<TEntity>> GetQueryableAsync<TEntity>(List<Expression<Func<TEntity, object>>> Includes) where TEntity : class
        {
            IQueryable<TEntity> items = Set<TEntity>();            
            Includes.ForEach(a => items = items.Include(a));             
            return await Task.FromResult(items);
        }

        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Casa> Casa { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Escuela> Escuela { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Matricula> Matricula { get; set; }
    }
}
