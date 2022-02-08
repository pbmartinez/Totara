using Domain.Entities;
using Domain.Infraestructure;
using Domain.IRepositories;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var item = await _unitOfWork.GetQueryable(Includes, a => a.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task UpdateAsync(TEntity item)
        {
            var a = await GetAsync(item.Id);
            _unitOfWork.GetEntry(a).CurrentValues.SetValues(item);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> FindWithSpecificationPatternAsync(Specification<TEntity> specification = null, List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = _unitOfWork.GetQueryable(Includes, specification.ToExpression());
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<TEntity>> FindWithExpressionAsync(Expression<Func<TEntity, bool>> expression, List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = _unitOfWork.GetQueryable(Includes, expression);
            return await Task.FromResult( items); 
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> Includes = null)
        {
            var items = _unitOfWork.GetQueryable(Includes);
            return await Task.FromResult(items);
        }

        public async Task<List<TEntity>> AllMatchingAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order)
        {
            var items = await _unitOfWork.GetQueryable(includes,predicate,  order).ToListAsync();
            return items;
        }

        public async Task<int> AllMatchingCountAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order)
        {
            var count = await _unitOfWork.GetQueryable(null, predicate).CountAsync();
            return count;
        }

        public async Task<PagedCollection<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order, int pageSize, int pageGo)
        {
            var items = await _unitOfWork.GetQueryable(includes, predicate, order).ToListAsync();
            var totalItems = await _unitOfWork.GetQueryable(null, predicate).CountAsync();

            var page = new PagedCollection<TEntity>() 
            {
                Items = items,
                Order = order,
                PageIndex = pageGo,
                PageSize = pageSize,
                TotalItems = totalItems
            };
            return page;
        }
    }
}
