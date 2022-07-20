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
using Application.Specifications;
using Application.IValidator;
using Application.Exceptions;

namespace Infrastructure.Application.AppServices
{
    public partial class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public UsuarioAppService(IUsuarioRepository UsuarioRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _UsuarioRepository = UsuarioRepository ?? throw new ArgumentNullException(nameof(UsuarioRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(UsuarioDto item, CancellationToken cancellationToken = default)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _UsuarioRepository.AddAsync(_mapper.Map<Usuario>(item), cancellationToken);
                commited = await _UsuarioRepository.UnitOfWork.CommitAsync(cancellationToken);
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<UsuarioDto>> FindAllBySpecificationPatternAsync(Specification<UsuarioDto>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<UsuarioDto>>(
                await _UsuarioRepository.FindAllByExpressionAsync(_mapper.MapExpression<Expression<Func<Usuario, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), includes, order, cancellationToken));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<UsuarioDto>? specification = null, CancellationToken cancellationToken = default)
        {
            var count = await _UsuarioRepository.FindCountByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Usuario>(), cancellationToken);
            return count;
        }

        public async Task<UsuarioDto?> FindOneBySpecificationPatternAsync(Specification<UsuarioDto>? specification = null, List<string>? includes = null, CancellationToken cancellationToken = default)
        {
            var item = await _UsuarioRepository.FindOneByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Usuario>(), includes, cancellationToken);
            return _mapper.Map<UsuarioDto>(item);
        }

        public async Task<List<UsuarioDto>> FindPageBySpecificationPatternAsync(Specification<UsuarioDto>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<UsuarioDto>>(
                await _UsuarioRepository.FindPageByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Usuario>(), includes ?? new List<string>(), order ?? new Dictionary<string, bool>(), pageSize, pageGo, cancellationToken));
        }


        public UsuarioDto Get(int id, List<string>? includes = null)
        {
            return _mapper.Map<UsuarioDto>(_UsuarioRepository.Get(id, includes));
        }

        public async Task<List<UsuarioDto>> GetAllAsync(List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            var items = await _UsuarioRepository.GetAllAsync(includes, order, cancellationToken);
            var dtoItems = _mapper.Map<List<UsuarioDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<UsuarioDto> GetAsync(int id, List<string>? includes = null, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<UsuarioDto>(await _UsuarioRepository.GetAsync(id, includes, cancellationToken));
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _UsuarioRepository.GetAsync(id, cancellationToken: cancellationToken);
            if (item != null)
                await _UsuarioRepository.DeleteAsync(item, cancellationToken);
            var commited = await _UsuarioRepository.UnitOfWork.CommitAsync(cancellationToken: cancellationToken);

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(UsuarioDto item, CancellationToken cancellationToken = default)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _UsuarioRepository.UpdateAsync(_mapper.Map<Usuario>(item), cancellationToken);
                commited = await _UsuarioRepository.UnitOfWork.CommitAsync(cancellationToken);
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
