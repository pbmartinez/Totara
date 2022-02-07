using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IAppServices
{
    public interface IAppService<TEntity, TEntityForCreation, TEntityForUpdate> 
    {
        Task<bool> AddAsync(TEntityForCreation item);
        Task<bool> UpdateAsync(TEntityForUpdate item);
        Task<TEntity> GetAsync(Guid id);
        Task<TEntityForUpdate> GetForUpdateAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>> Includes);
        Task<List<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> Includes);
        Task<bool> RemoveAsync(Guid id);
        Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(Specification<TEntity> specification = null);
    }
}
