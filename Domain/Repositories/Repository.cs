using Domain.Entities;
using Domain.IRepositories;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task AddAsync(TEntity item)
        {
            await _unitOfWork.Repository<TEntity>().AddAsync(item);
        }

        public async Task DeleteAsync(TEntity item)
        {
            await Task.FromResult(_unitOfWork.Repository<TEntity>().Remove(item));
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult<IQueryable<TEntity>>(_unitOfWork.Repository<TEntity>());
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Task.FromResult(_unitOfWork.Repository<TEntity>().FirstOrDefault(a => a.Id == id));
        }

        public async Task UpdateAsync(TEntity item)
        {
            var a = await GetAsync(item.Id);
            _unitOfWork.GetEntry(a).CurrentValues.SetValues(item);
            await Task.CompletedTask;
        }
        public IEnumerable<TEntity> FindWithSpecificationPattern(IExpressionSpecification<TEntity> specification = null)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_unitOfWork.Repository<TEntity>().AsQueryable(), specification);
        }
    }
}
