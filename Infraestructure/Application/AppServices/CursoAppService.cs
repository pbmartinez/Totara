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
    public partial class CursoAppService : ICursoAppService
    {
        private readonly IMapper _mapper;
        private readonly ICursoRepository _CursoRepository;

        public CursoAppService(IMapper mapper, ICursoRepository CursoRepository)
        {
            _mapper = mapper;
            _CursoRepository = CursoRepository;
        }

        public async Task<bool> AddAsync(CursoDtoForCreate item)
        {
            await _CursoRepository.AddAsync(_mapper.Map<Curso>(item));
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<CursoDto>> FindAllBySpecificationPatternAsync(Specification<CursoDto> specification = null, List<Expression<Func<CursoDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(includes).ToList();
            return _mapper.Map<List<CursoDto>>(
                await _CursoRepository.FindAllByExpressionAsync(
                    specification.MapToExpressionOfType<Curso>(), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<CursoDto> specification = null)
        {
            var count = await _CursoRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Curso>());
            return count;
        }

        public async Task<CursoDto> FindOneBySpecificationPatternAsync(Specification<CursoDto> specification = null, List<Expression<Func<CursoDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(includes).ToList();
            var item = await _CursoRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Curso>(), domainExpressionIncludesList);
            return _mapper.Map<CursoDto>(item);
        }

        public async Task<List<CursoDto>> FindPageBySpecificationPatternAsync(Specification<CursoDto> specification = null, List<Expression<Func<CursoDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(includes).ToList();
            return _mapper.Map<List<CursoDto>>(
                await _CursoRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Curso>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }



        public async Task<List<CursoDto>> GetAllAsync(List<Expression<Func<CursoDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(includes).ToList();
            var items = await _CursoRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<CursoDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<CursoDto> GetAsync(Guid id, List<Expression<Func<CursoDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(Includes).ToList();
            return _mapper.Map<CursoDto>(await _CursoRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<CursoDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<CursoDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Curso, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Curso, object>>>(Includes).ToList();
            return _mapper.Map<CursoDtoForUpdate>(await _CursoRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _CursoRepository.GetAsync(id);
            await _CursoRepository.DeleteAsync(item);
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(CursoDtoForUpdate item)
        {
            await _CursoRepository.UpdateAsync(_mapper.Map<Curso>(item));
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
