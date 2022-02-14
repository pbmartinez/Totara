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
    public partial class EscuelaAppService : IEscuelaAppService
    {
        private readonly IMapper _mapper;
        private readonly IEscuelaRepository _EscuelaRepository;

        public EscuelaAppService(IMapper mapper, IEscuelaRepository EscuelaRepository)
        {
            _mapper = mapper;
            _EscuelaRepository = EscuelaRepository;
        }

        public async Task<bool> AddAsync(EscuelaDtoForCreate item)
        {
            await _EscuelaRepository.AddAsync(_mapper.Map<Escuela>(item));
            var commited = await _EscuelaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<EscuelaDto>> FindAllBySpecificationPatternAsync(Specification<EscuelaDto> specification = null, List<Expression<Func<EscuelaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(includes).ToList();
            return _mapper.Map<List<EscuelaDto>>(
                await _EscuelaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Escuela, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<EscuelaDto> specification = null)
        {
            var count = await _EscuelaRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Escuela>());
            return count;
        }

        public async Task<EscuelaDto> FindOneBySpecificationPatternAsync(Specification<EscuelaDto> specification = null, List<Expression<Func<EscuelaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(includes).ToList();
            var item = await _EscuelaRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Escuela>(), domainExpressionIncludesList);
            return _mapper.Map<EscuelaDto>(item);
        }

        public async Task<List<EscuelaDto>> FindPageBySpecificationPatternAsync(Specification<EscuelaDto> specification = null, List<Expression<Func<EscuelaDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(includes).ToList();
            return _mapper.Map<List<EscuelaDto>>(
                await _EscuelaRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Escuela>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }


        public async Task<List<EscuelaDto>> GetAllAsync(List<Expression<Func<EscuelaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(includes).ToList();
            var items = await _EscuelaRepository.GetAllAsync(domainExpressionIncludesList);
            var dtoItems = _mapper.Map<List<EscuelaDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<EscuelaDto> GetAsync(Guid id, List<Expression<Func<EscuelaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(Includes).ToList();
            return _mapper.Map<EscuelaDto>(await _EscuelaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<EscuelaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<EscuelaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Escuela, object>>>(Includes).ToList();
            return _mapper.Map<EscuelaDtoForUpdate>(await _EscuelaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _EscuelaRepository.GetAsync(id);
            await _EscuelaRepository.DeleteAsync(item);
            var commited = await _EscuelaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(EscuelaDtoForUpdate item)
        {
            await _EscuelaRepository.UpdateAsync(_mapper.Map<Escuela>(item));
            var commited = await _EscuelaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
