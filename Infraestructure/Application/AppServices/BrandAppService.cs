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
    public partial class BrandAppService : IBrandAppService
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public BrandAppService(IBrandRepository BrandRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _BrandRepository = BrandRepository ?? throw new ArgumentNullException(nameof(BrandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(BrandDto item, CancellationToken cancellationToken = default)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BrandRepository.AddAsync(_mapper.Map<Brand>(item), cancellationToken);
                commited = await _BrandRepository.UnitOfWork.CommitAsync(cancellationToken);
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<BrandDto>> FindAllBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<BrandDto>>(
                await _BrandRepository.FindAllByExpressionAsync(_mapper.MapExpression<Expression<Func<Brand, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), includes, order, cancellationToken));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<BrandDto>? specification = null, CancellationToken cancellationToken = default)
        {
            var count = await _BrandRepository.FindCountByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Brand>(), cancellationToken);
            return count;
        }

        public async Task<BrandDto?> FindOneBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<string>? includes = null, CancellationToken cancellationToken = default)
        {
            var item = await _BrandRepository.FindOneByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Brand>(), includes, cancellationToken);
            return _mapper.Map<BrandDto>(item);
        }

        public async Task<List<BrandDto>> FindPageBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<string>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<BrandDto>>(
                await _BrandRepository.FindPageByExpressionAsync(specification == null ? a => true : specification.MapToExpressionOfType<Brand>(), includes ?? new List<string>(), order ?? new Dictionary<string, bool>(), pageSize, pageGo, cancellationToken));
        }


        public BrandDto Get(Guid id, List<string>? includes = null)
        {
            return _mapper.Map<BrandDto>(_BrandRepository.Get(id, includes));
        }

        public async Task<List<BrandDto>> GetAllAsync(List<string>? includes = null, Dictionary<string, bool>? order = null, CancellationToken cancellationToken = default)
        {
            var items = await _BrandRepository.GetAllAsync(includes, order, cancellationToken);
            var dtoItems = _mapper.Map<List<BrandDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<BrandDto> GetAsync(Guid id, List<string>? includes = null, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<BrandDto>(await _BrandRepository.GetAsync(id, includes, cancellationToken));
        }

        public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await _BrandRepository.GetAsync(id, cancellationToken: cancellationToken);
            if (item != null)
                await _BrandRepository.DeleteAsync(item, cancellationToken);
            var commited = await _BrandRepository.UnitOfWork.CommitAsync(cancellationToken: cancellationToken);

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(BrandDto item, CancellationToken cancellationToken = default)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BrandRepository.UpdateAsync(_mapper.Map<Brand>(item), cancellationToken);
                commited = await _BrandRepository.UnitOfWork.CommitAsync(cancellationToken);
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
