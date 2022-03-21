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

namespace Infraestructure.Application.AppServices
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

        public async Task<bool> AddAsync(BrandDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BrandRepository.AddAsync(_mapper.Map<Brand>(item));
                commited = await _BrandRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<BrandDto>> FindAllBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<Expression<Func<BrandDto, object>>>? includes = null, Dictionary<string, bool>? order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            return _mapper.Map<List<BrandDto>>(
                await _BrandRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Brand, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<BrandDto>? specification = null)
        {
            var count = await _BrandRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<Brand>());
            return count;
        }

        public async Task<BrandDto> FindOneBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<Expression<Func<BrandDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            var item = await _BrandRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<Brand>(), domainExpressionIncludesList);
            return _mapper.Map<BrandDto>(item);
        }

        public async Task<List<BrandDto>> FindPageBySpecificationPatternAsync(Specification<BrandDto>? specification = null, List<Expression<Func<BrandDto, object>>>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            return _mapper.Map<List<BrandDto>>(
                await _BrandRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<Brand>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public BrandDto Get(Guid id, List<Expression<Func<BrandDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            return _mapper.Map<BrandDto>(_BrandRepository.Get(id, domainExpressionIncludesList));
        }

        public async Task<List<BrandDto>> GetAllAsync(List<Expression<Func<BrandDto, object>>>? includes, Dictionary<string, bool>? order)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            var items = await _BrandRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<BrandDto>>(items.ToList());
            return dtoItems;
        }



        public async Task<BrandDto> GetAsync(Guid id, List<Expression<Func<BrandDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Brand, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
            return _mapper.Map<BrandDto>(await _BrandRepository.GetAsync(id, domainExpressionIncludesList));
        }


        //public async Task<BrandDto> GetForUpdateAsync(Guid id, List<Expression<Func<BrandDto, object>>>? includes = null)
        //{
        //    var domainExpressionIncludesList = includes == null
        //        ? new List<Expression<Func<Brand, object>>>()
        //        : _mapper.MapIncludesList<Expression<Func<Brand, object>>>(includes).ToList();
        //    return _mapper.Map<BrandDto>(await _BrandRepository.GetAsync(id, domainExpressionIncludesList));
        //}

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _BrandRepository.GetAsync(id);
            await _BrandRepository.DeleteAsync(item);
            var commited = await _BrandRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(BrandDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BrandRepository.UpdateAsync(_mapper.Map<Brand>(item));
                commited = await _BrandRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
