using Domain.Entities;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IRepository <TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task AddAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(TEntity item);
        //From Mukesh Murugan pendiente evaluar
        IEnumerable<TEntity> FindWithSpecificationPattern(IExpressionSpecification<TEntity> specification = null);

        //From Vladimir Khorikov
        IEnumerable<TEntity> FindWithSpecificationPattern(BaseSpecification<TEntity> specification = null);
    }
}
