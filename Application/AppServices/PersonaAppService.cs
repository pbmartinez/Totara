using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using Domain.IRepositories;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public partial class PersonaAppService : IPersonaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPersonaRepository _personaRepository;

        public PersonaAppService(IMapper mapper, IPersonaRepository personaRepository)
        {
            _mapper = mapper;
            _personaRepository = personaRepository;
        }

        public async Task<bool> AddAsync(PersonaDtoForCreate item)
        {
            await _personaRepository.AddAsync(_mapper.Map<Domain.Entities.Persona>(item));
            var commited = await _personaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public IEnumerable<PersonaDto> FindWithSpecificationPattern(IExpressionSpecification<PersonaDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PersonaDto>> GetAllAsync()
        {
            return _mapper.Map<List<PersonaDto>>(await _personaRepository.GetAllAsync());
        }

        public async Task<PersonaDto> GetAsync(Guid id)
        {
            return _mapper.Map<PersonaDto>(await _personaRepository.GetAsync(id));
        }

        public async Task<PersonaDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            return _mapper.Map<PersonaDtoForUpdate>(await _personaRepository.GetAsync(id));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _personaRepository.GetAsync(id);
            await _personaRepository.DeleteAsync(item);
            var commited = await _personaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(PersonaDtoForUpdate item)
        {
            await _personaRepository.UpdateAsync(_mapper.Map<Domain.Entities.Persona>(item));
            var commited = await _personaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
