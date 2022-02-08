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
using Application.Specification;

namespace Application.AppServices
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
            await _casaRepository.AddAsync(_mapper.Map<Domain.Entities.Casa>(item));
            var commited = await _casaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<IEnumerable<CasaDto>> FindWithSpecificationPatternAsync(Specification<CasaDto> specification = null)
        {
            return _mapper.Map<List<CasaDto>>(
                await _casaRepository.FindWithExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Casa, bool>>>(
                        specification == null ? a => true : specification.ToExpression())));
        }

        public async Task<List<CasaDto>> GetAllAsync(List<Expression<Func<CasaDto, object>>> Includes)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Casa, object>>>(Includes).ToList();
            var items = await _casaRepository.GetAllAsync(domainExpressionList);
            var dtoItems = _mapper.Map<List<CasaDto>>(items.ToList());
            return dtoItems;
        }

        

        public async Task<CasaDto> GetAsync(Guid id, List<Expression<Func<CasaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Casa, object>>>(Includes).ToList();
            return _mapper.Map<CasaDto>(await _casaRepository.GetAsync(id, domainExpressionList));
        }


        public async Task<CasaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<CasaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Casa, object>>>(Includes).ToList();
            return _mapper.Map<CasaDtoForUpdate>(await _casaRepository.GetAsync(id, domainExpressionList));
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
            await _casaRepository.UpdateAsync(_mapper.Map<Domain.Entities.Casa>(item));
            var commited = await _casaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
