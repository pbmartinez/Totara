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

        public async Task<IEnumerable<PersonaDto>> FindWithSpecificationPatternAsync(Specification<PersonaDto> specification = null)
        {
            return _mapper.Map<List<PersonaDto>>(
                await _personaRepository.FindWithExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Persona, bool>>>(
                        specification == null ? a => true : specification.ToExpression())));
        }

        public async Task<List<PersonaDto>> GetAllAsync(List<Expression<Func<PersonaDto, object>>> Includes)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Persona, object>>>(Includes).ToList();
            var items = await _personaRepository.GetAllAsync(domainExpressionList);
            var dtoItems = _mapper.Map<List<PersonaDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<PersonaDto> GetAsync(Guid id, List<Expression<Func<PersonaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Persona, object>>>(Includes).ToList();
            return _mapper.Map<PersonaDto>(await _personaRepository.GetAsync(id, domainExpressionList));
        }

        public async Task<PersonaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<PersonaDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Persona, object>>>(Includes).ToList();
            return _mapper.Map<PersonaDtoForUpdate>(await _personaRepository.GetAsync(id, domainExpressionList));
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
