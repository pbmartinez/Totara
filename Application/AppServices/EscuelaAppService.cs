using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using Domain.IRepositories;
using Domain.Specification;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<EscuelaDto>> FindWithSpecificationPatternAsync(Specification<EscuelaDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EscuelaDto>> GetAllAsync()
        {
            return _mapper.Map<List<EscuelaDto>>(await _EscuelaRepository.GetAllAsync());
        }

        
        public Task<List<EscuelaDto>> GetAllAsync(Expression<Func<EscuelaDto, object>> Includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<EscuelaDto>> GetAllAsync(List<Expression<Func<EscuelaDto, object>>> Includes)
        {
            throw new NotImplementedException();
        }

        public async Task<EscuelaDto> GetAsync(Guid id)
        {
            return _mapper.Map<EscuelaDto>(await _EscuelaRepository.GetAsync(id));
        }

        public async Task<EscuelaDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            return _mapper.Map<EscuelaDtoForUpdate>(await _EscuelaRepository.GetAsync(id));
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
