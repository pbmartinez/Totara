using Domain.Entities;
using Domain.Utils;
using Domain.IRepositories;
using Domain.Specification;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Base;

namespace Infrastructure.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public async Task AddAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Repository<TEntity>().AddAsync(item, cancellationToken);
        }

        public async Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(_unitOfWork.Repository<TEntity>().Remove(item));
        }

        

        public async Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            var a = await GetAsync(item.Id);
            _unitOfWork.GetEntry(a).CurrentValues.SetValues(item);
            await Task.CompletedTask;
        }



        public async Task<int> FindCountByExpressionAsync(Expression<Func<TEntity, bool>>? expression, CancellationToken cancellationToken = default)
        {
            var count = await _unitOfWork.GetQueryable(null, expression).CountAsync(cancellationToken: cancellationToken);
            return count;
        }


        public TEntity Get(Guid id, List<string>? includes = null)
        {
            var item = _unitOfWork.GetQueryable(includes,(Expression<Func<TEntity,bool>>) (a => a.Id == id)).FirstOrDefault();
            return item;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            var items = _unitOfWork.GetQueryable(includes, (Expression<Func<TEntity, bool>>)(a => true), order,0,0);
            return (IQueryable<TEntity>)await Task.FromResult(items);
        }

        public async Task<TEntity> GetAsync(Guid id, List<string>? includes = null, CancellationToken cancellationToken = default)
        {
            var i = includes == null ? new List<string>() : includes.ToList();
            var item = await _unitOfWork.GetQueryable(i, (Expression<Func<TEntity, bool>>)(a => a.Id == id))
                .FirstOrDefaultAsync(cancellationToken:cancellationToken);
            return item;
        }

        public async Task<TEntity> FindOneByExpressionAsync(Expression<Func<TEntity, bool>>? expression, List<string>? includes, CancellationToken cancellationToken = default)
        {
            var item = await _unitOfWork.GetQueryable(includes, expression).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return item;
        }

        public async Task<PagedCollection<TEntity>> FindPageByExpressionAsync(Expression<Func<TEntity, bool>>? expression, List<string>? includes, Dictionary<string, bool>? order, int pageSize, int pageGo, CancellationToken cancellationToken = default)
        {
            if (order == null || order.Count == 0)
                order = new Dictionary<string, bool>() { { "Id", true } };
            var items = await _unitOfWork.GetQueryable(includes, expression, order, pageSize, pageGo).ToListAsync(cancellationToken: cancellationToken);
            var totalItems = await _unitOfWork.GetQueryable(null, expression).CountAsync(cancellationToken: cancellationToken);

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

        public async Task<List<TEntity>> FindAllByExpressionAsync(Expression<Func<TEntity, bool>>? expression, List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            var items = await _unitOfWork.GetQueryable(includes, expression, order).ToListAsync(cancellationToken: cancellationToken);
            return items;
        }
    }
}
