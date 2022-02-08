using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.IRepositories;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.AppServices
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
            await _EscuelaRepository.AddAsync(_mapper.Map<Domain.Entities.Escuela>(item));
            var commited = await _EscuelaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<IEnumerable<EscuelaDto>> FindWithSpecificationPatternAsync(Specification<EscuelaDto> specification = null)
        {
            return _mapper.Map<List<EscuelaDto>>(
                await _EscuelaRepository.FindWithExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Escuela, bool>>>(
                        specification == null ? a => true : specification.ToExpression())));
        }

        public async Task<List<EscuelaDto>> GetAllAsync(List<Expression<Func<EscuelaDto, object>>> Includes)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Escuela, object>>>(Includes).ToList();
            var items = await _EscuelaRepository.GetAllAsync(domainExpressionList);
            var dtoItems = _mapper.Map<List<EscuelaDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<EscuelaDto> GetAsync(Guid id, List<Expression<Func<EscuelaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Escuela, object>>>(Includes).ToList();
            return _mapper.Map<EscuelaDto>(await _EscuelaRepository.GetAsync(id, domainExpressionList));
        }

        public async Task<EscuelaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<EscuelaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Escuela, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Escuela, object>>>(Includes).ToList();
            return _mapper.Map<EscuelaDtoForUpdate>(await _EscuelaRepository.GetAsync(id, domainExpressionList));
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
            await _EscuelaRepository.UpdateAsync(_mapper.Map<Domain.Entities.Escuela>(item));
            var commited = await _EscuelaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
