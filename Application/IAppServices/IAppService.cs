using Application.IValidator;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IAppServices
{
    public interface IAppService<TEntity, TKey>
    {
        TEntity Get(TKey id, List<string>? includes = null);
        Task<bool> AddAsync(TEntity item, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TEntity item, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(TKey id, List<string>? includes = null, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity?> FindOneBySpecificationPatternAsync(Specification<TEntity>? specification = null, List<string>? includes = null, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindPageBySpecificationPatternAsync(Specification<TEntity>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllBySpecificationPatternAsync(Specification<TEntity>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default);
        Task<int> FindCountBySpecificationPatternAsync(Specification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    }
}
