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
using static Domain.Specification.BaseSpecification;

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

        public IEnumerable<CasaDto> FindWithSpecificationPattern(IExpressionSpecification<CasaDto> specification = null)
        {            
            var exp = _mapper.MapExpression<Expression<Func<Domain.Entities.Casa, bool>>>(specification.Criteria);
            var domainSpecification = new BaseSpecifcation<Domain.Entities.Casa>(exp);
            var items = _casaRepository.FindWithSpecificationPattern(domainSpecification);
            return _mapper.Map<List<CasaDto>>(items);
        }

        public async Task<List<CasaDto>> GetAllAsync()
        {
            return _mapper.Map<List<CasaDto>>(await _casaRepository.GetAllAsync());
        }

        public async Task<CasaDto> GetAsync(Guid id)
        {
            return _mapper.Map<CasaDto>(await _casaRepository.GetAsync(id));
        }

        public Task<CasaDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _casaRepository.GetAsync(id);
            await _casaRepository.DeleteAsync(item);
            var commited = await _casaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public Task<bool> UpdateAsync(CasaDtoForUpdate item)
        {
            throw new NotImplementedException();
        }
    }
}
