﻿using Domain.Entities;
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

        

        public async Task<IQueryable<TEntity>> GetAllAsync(List<Expression<Func<TEntity, object>>> includes, Dictionary<string,bool>order)
        {
            var items = _unitOfWork.GetQueryable(includes, a => true, order);
            return await Task.FromResult(items);
        }


        

        public async Task<TEntity> FindOneBySpecificationPatternAsync(Specification<TEntity> specification, List<Expression<Func<TEntity, object>>> includes)
        {
            var item = await _unitOfWork.GetQueryable(includes, specification.ToExpression()).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> FindAllBySpecificationPatternAsync(Specification<TEntity> specification, List<Expression<Func<TEntity, object>>> includes)
        {
            var items = await _unitOfWork.GetQueryable(includes, specification.ToExpression()).ToListAsync();
            return items;
        }

        public async Task<PagedCollection<TEntity>> FindPageBySpecificationPatternAsync(Specification<TEntity> specification, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order, int pageSize, int pageGo)
        {
            var items = await _unitOfWork.GetQueryable(includes, specification.ToExpression(), order).ToListAsync();
            var totalItems = await _unitOfWork.GetQueryable(null, specification.ToExpression()).CountAsync();

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

        public async Task<TEntity> FindOneByExpressionAsync(Expression<Func<TEntity, bool>> expression, List<Expression<Func<TEntity, object>>> includes)
        {
            var item = await _unitOfWork.GetQueryable(includes, expression).FirstOrDefaultAsync();
            return item;
        }

        public async Task<PagedCollection<TEntity>> FindPageByExpressionAsync(Expression<Func<TEntity, bool>> expression, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order, int pageSize, int pageGo)
        {
            var items = await _unitOfWork.GetQueryable(includes, expression, order, pageSize, pageGo).ToListAsync();
            var totalItems = await _unitOfWork.GetQueryable(null, expression).CountAsync();

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

        public async Task<List<TEntity>> FindAllByExpressionAsync(Expression<Func<TEntity, bool>> expression, List<Expression<Func<TEntity, object>>> includes, Dictionary<string, bool> order)
        {
            var items = await _unitOfWork.GetQueryable(includes, expression, order).ToListAsync();
            return items;
        }

        public async Task<int> FindCountByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            var count = await _unitOfWork.GetQueryable(null, expression).CountAsync();
            return count;
        }
    }
}
