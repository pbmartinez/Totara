using Domain.Entities;
using Domain.Infraestructure;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IRepository <TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<List<TEntity>> AllMatchingAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order);
        Task<int> AllMatchingCountAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order);
        Task<PagedCollection<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order, int pageSize, int pageGo);

        Task<IQueryable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> Includes = null);
        Task<TEntity> GetAsync(Guid id, List<Expression<Func<TEntity, object>>> Includes = null);
        Task AddAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(TEntity item);
        
        //From Vladimir Khorikov
        Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> Includes = null);

        Task<IEnumerable<TEntity>> FindWithExpressionAsync(Expression<Func<TEntity,bool>> expression, List<Expression<Func<TEntity, object>>> Includes = null);
    }
}
