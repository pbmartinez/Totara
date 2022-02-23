using Application.IValidator;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IAppServices
{
    public interface IAppService<TEntity>
    {
        TEntity Get(Guid id, List<Expression<Func<TEntity, object>>> includes = null);
        Task<bool> AddAsync(TEntity item);
        Task<bool> UpdateAsync(TEntity item);
        Task<TEntity> GetAsync(Guid id, List<Expression<Func<TEntity, object>>> includes = null);
//        Task<TEntityForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<TEntity, object>>> includes = null);
        Task<List<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> includes = null, Dictionary<string, bool> order = null);
        Task<bool> RemoveAsync(Guid id);
        Task<TEntity> FindOneBySpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> includes = null);
        Task<List<TEntity>> FindPageBySpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0);
        Task<List<TEntity>> FindAllBySpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> includes = null, Dictionary<string, bool> order = null);
        Task<int> FindCountBySpecificationPatternAsync(Specification<TEntity> specification = null);
    }
}
