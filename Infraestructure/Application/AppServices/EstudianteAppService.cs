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
    public partial class EstudianteAppService : IEstudianteAppService
    {
        private readonly IMapper _mapper;
        private readonly IEstudianteRepository _EstudianteRepository;
        private readonly IEntityValidator _entityValidator;

        public EstudianteAppService(IMapper mapper, IEstudianteRepository estudianteRepository, IEntityValidator entityValidator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _EstudianteRepository = estudianteRepository ?? throw new ArgumentNullException(nameof(estudianteRepository));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(EstudianteDtoForCreate item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _EstudianteRepository.AddAsync(_mapper.Map<Estudiante>(item));
                commited = await _EstudianteRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<EstudianteDto>> FindAllBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
               ? new List<Expression<Func<Estudiante, object>>>()
               : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(includes).ToList();
            return _mapper.Map<List<EstudianteDto>>(
                await _EstudianteRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Estudiante, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<EstudianteDto> specification = null)
        {
            var count = await _EstudianteRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Estudiante>());
            return count;
        }

        public async Task<EstudianteDto> FindOneBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(includes).ToList();
            var item = await _EstudianteRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Estudiante>(), domainExpressionIncludesList);
            return _mapper.Map<EstudianteDto>(item);
        }

        public async Task<List<EstudianteDto>> FindPageBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(includes).ToList();
            return _mapper.Map<List<EstudianteDto>>(
                await _EstudianteRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Estudiante>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }



        public async Task<List<EstudianteDto>> GetAllAsync(List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(includes).ToList();
            var items = await _EstudianteRepository.GetAllAsync(domainExpressionIncludesList);
            var dtoItems = _mapper.Map<List<EstudianteDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<EstudianteDto> GetAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(Includes).ToList();
            return _mapper.Map<EstudianteDto>(await _EstudianteRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<EstudianteDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Estudiante, object>>>(Includes).ToList();
            return _mapper.Map<EstudianteDtoForUpdate>(await _EstudianteRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _EstudianteRepository.GetAsync(id);
            await _EstudianteRepository.DeleteAsync(item);
            var commited = await _EstudianteRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<bool> UpdateAsync(EstudianteDtoForUpdate item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _EstudianteRepository.UpdateAsync(_mapper.Map<Estudiante>(item));
                commited = await _EstudianteRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }
    }
}
