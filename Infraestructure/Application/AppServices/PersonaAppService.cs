using Application.Dtos;
using Application.Exceptions;
using Application.IAppServices;
using Application.IValidator;
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
    public partial class PersonaAppService : IPersonaAppService
    {
        private readonly IMapper _mapper;
        private readonly IPersonaRepository _personaRepository;
        private readonly IEntityValidator _entityValidator;

        public PersonaAppService(IMapper mapper, IPersonaRepository personaRepository, IEntityValidator entityValidator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personaRepository = personaRepository ?? throw new ArgumentNullException(nameof(personaRepository));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(PersonaDtoForCreate item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _personaRepository.AddAsync(_mapper.Map<Persona>(item));
                commited = await _personaRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<PersonaDto>> FindAllBySpecificationPatternAsync(Specification<PersonaDto> specification = null, List<Expression<Func<PersonaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(includes).ToList();
            return _mapper.Map<List<PersonaDto>>(
                await _personaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Persona, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<PersonaDto> specification = null)
        {
            var count = await _personaRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Persona>());
            return count;
        }

        public async Task<PersonaDto> FindOneBySpecificationPatternAsync(Specification<PersonaDto> specification = null, List<Expression<Func<PersonaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(includes).ToList();
            var item = await _personaRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Persona>(), domainExpressionIncludesList);
            return _mapper.Map<PersonaDto>(item);
        }

        public async Task<List<PersonaDto>> FindPageBySpecificationPatternAsync(Specification<PersonaDto> specification = null, List<Expression<Func<PersonaDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(includes).ToList();
            return _mapper.Map<List<PersonaDto>>(
                await _personaRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Persona>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public async Task<List<PersonaDto>> GetAllAsync(List<Expression<Func<PersonaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(includes).ToList();
            var items = await _personaRepository.GetAllAsync(domainExpressionIncludesList);
            var dtoItems = _mapper.Map<List<PersonaDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<PersonaDto> GetAsync(Guid id, List<Expression<Func<PersonaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(Includes).ToList();
            return _mapper.Map<PersonaDto>(await _personaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<PersonaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<PersonaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Persona, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Persona, object>>>(Includes).ToList();
            return _mapper.Map<PersonaDtoForUpdate>(await _personaRepository.GetAsync(id, domainExpressionIncludesList));
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
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _personaRepository.UpdateAsync(_mapper.Map<Persona>(item));
                commited = await _personaRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }
    }
}
