using Domain.Specification;
using System;
using System.Collections.Generic;
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
        Task<bool> RemoveAsync(Guid id);
        IEnumerable<TEntity> FindWithSpecificationPattern(Specification<TEntity> specification = null);
    }
}
