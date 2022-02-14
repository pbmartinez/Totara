using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.IRepositories;
using Domain.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Specification;

namespace Infraestructure.Application.AppServices
{
    public partial class CasaAppService : ICasaAppService
    {
        private readonly ICasaRepository _casaRepository;
        private readonly IMapper _mapper;

        public CasaAppService(ICasaRepository casaRepository, IMapper mapper)
        {
            _casaRepository = casaRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(CasaDtoForCreate item)
        {
            await _casaRepository.AddAsync(_mapper.Map<Casa>(item));
            var commited = await _casaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<CasaDto>> FindAllBySpecificationPatternAsync(Specification<CasaDto> specification = null, List<Expression<Func<CasaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<List<CasaDto>>(
                await _casaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Casa, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<CasaDto> specification = null)
        {
            var count = await _casaRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Casa>());
            return count;
        }

        public async Task<CasaDto> FindOneBySpecificationPatternAsync(Specification<CasaDto> specification = null, List<Expression<Func<CasaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            var item = await _casaRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Casa>(), domainExpressionIncludesList);
            return _mapper.Map<CasaDto>(item);
        }

        public async Task<List<CasaDto>> FindPageBySpecificationPatternAsync(Specification<CasaDto> specification = null, List<Expression<Func<CasaDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<List<CasaDto>>(
                await _casaRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Casa>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public async Task<List<CasaDto>> GetAllAsync(List<Expression<Func<CasaDto, object>>> includes, Dictionary<string, bool> order)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            var items = await _casaRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<CasaDto>>(items.ToList());
            return dtoItems;
        }



        public async Task<CasaDto> GetAsync(Guid id, List<Expression<Func<CasaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<CasaDto>(await _casaRepository.GetAsync(id, domainExpressionIncludesList));
        }


        public async Task<CasaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<CasaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<CasaDtoForUpdate>(await _casaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _casaRepository.GetAsync(id);
            await _casaRepository.DeleteAsync(item);
            var commited = await _casaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(CasaDtoForUpdate item)
        {
            await _casaRepository.UpdateAsync(_mapper.Map<Casa>(item));
            var commited = await _casaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
