using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Entities;
using Domain.IRepositories;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infraestructure.Application.AppServices
{
    public class CategoriaAppService : ICategoriaAppService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaAppService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddAsync(CategoriaDtoForCreate item)
        {
            await _categoriaRepository.AddAsync(_mapper.Map<Categoria>(item));
            var commited = await _categoriaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<CategoriaDto>> FindAllBySpecificationPatternAsync(Specification<CategoriaDto> specification = null, List<Expression<Func<CategoriaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            return _mapper.Map<List<CategoriaDto>>(
                await _categoriaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Categoria, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<CategoriaDto> specification = null)
        {
            var count = await _categoriaRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Categoria>());
            return count;
        }

        public async Task<CategoriaDto> FindOneBySpecificationPatternAsync(Specification<CategoriaDto> specification = null, List<Expression<Func<CategoriaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            var item = await _categoriaRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Categoria>(), domainExpressionIncludesList);
            return _mapper.Map<CategoriaDto>(item);
        }

        public async Task<List<CategoriaDto>> FindPageBySpecificationPatternAsync(Specification<CategoriaDto> specification = null, List<Expression<Func<CategoriaDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            return _mapper.Map<List<CategoriaDto>>(
                await _categoriaRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Categoria>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public async Task<List<CategoriaDto>> GetAllAsync(List<Expression<Func<CategoriaDto, object>>> includes, Dictionary<string, bool> order)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            var items = await _categoriaRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<CategoriaDto>>(items.ToList());
            return dtoItems;
        }



        public async Task<CategoriaDto> GetAsync(Guid id, List<Expression<Func<CategoriaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            return _mapper.Map<CategoriaDto>(await _categoriaRepository.GetAsync(id, domainExpressionIncludesList));
        }


        public async Task<CategoriaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<CategoriaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Categoria, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Categoria, object>>>(includes).ToList();
            return _mapper.Map<CategoriaDtoForUpdate>(await _categoriaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _categoriaRepository.GetAsync(id);
            await _categoriaRepository.DeleteAsync(item);
            var commited = await _categoriaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(CategoriaDtoForUpdate item)
        {
            await _categoriaRepository.UpdateAsync(_mapper.Map<Categoria>(item));
            var commited = await _categoriaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
