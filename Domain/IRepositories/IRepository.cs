using Domain.Entities;
using Domain.Utils;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using System.Threading;

namespace Domain.IRepositories
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Get(TKey id, List<string> includes = null);
        Task<IQueryable<TEntity>> GetAllAsync(List<string> includes = null, Dictionary<string, bool> order = null, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(TKey id, List<string> includes = null, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity item, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default);

        //All Searches by Specification Pattern in Service layer is translated in searches by expression in Repostory Layer
        //FindOneBySpecificationPatternAsync -> FindOneByExpressionAsync
        //The expression that is defined in the specification
        
        Task<TEntity> FindOneByExpressionAsync(Expression<Func<TEntity,bool>> expression, List<string> includes, CancellationToken cancellationToken = default);
        Task<PagedCollection<TEntity>> FindPageByExpressionAsync(Expression<Func<TEntity, bool>> expression, List<string> includes, Dictionary<string, bool> order, int pageSize, int pageGo, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllByExpressionAsync(Expression<Func<TEntity, bool>> expression, List<string> includes = null, Dictionary<string, bool> order = null, CancellationToken cancellationToken = default);
        Task<int> FindCountByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    }
}
