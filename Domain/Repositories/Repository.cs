using Domain.Entities;
using Domain.IRepositories;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<TEntity> GetAsync(Guid id, List<Expression<Func<TEntity, object>>> Includes = null)
        {
            return await Task.FromResult(_unitOfWork.Repository<TEntity>().FirstOrDefault(a => a.Id == id));
        }

        public async Task UpdateAsync(TEntity item)
        {
            var a = await GetAsync(item.Id);
            _unitOfWork.GetEntry(a).CurrentValues.SetValues(item);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = await _unitOfWork.GetQueryableAsync(Includes, specification.ToExpression());
            return items;
        }

        public async Task<IEnumerable<TEntity>> FindWithExpressionAsync(Expression<Func<TEntity, bool>> expression, List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = await _unitOfWork.GetQueryableAsync(Includes, expression);
            return items; 
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = await _unitOfWork.GetQueryableAsync(Includes);
            return (IQueryable<TEntity>)items;
        }
    }
}
