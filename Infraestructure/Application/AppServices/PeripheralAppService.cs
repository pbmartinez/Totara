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
using Application.Specification;
using Application.IValidator;
using Application.Exceptions;

namespace Infraestructure.Application.AppServices
{
    public partial class PeripheralAppService : IPeripheralAppService
    {
        private readonly IPeripheralRepository _PeripheralRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public PeripheralAppService(IPeripheralRepository peripheralRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _PeripheralRepository = peripheralRepository ?? throw new ArgumentNullException(nameof(peripheralRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(PeripheralDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _PeripheralRepository.AddAsync(_mapper.Map<Peripheral>(item));
                commited = await _PeripheralRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<PeripheralDto>> FindAllBySpecificationPatternAsync(Specification<PeripheralDto>? specification = null, List<Expression<Func<PeripheralDto, object>>>? includes = null, Dictionary<string, bool>? order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            return _mapper.Map<List<PeripheralDto>>(
                await _PeripheralRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Peripheral, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<PeripheralDto>? specification = null)
        {
            var count = await _PeripheralRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<Peripheral>());
            return count;
        }

        public async Task<PeripheralDto> FindOneBySpecificationPatternAsync(Specification<PeripheralDto>? specification = null, List<Expression<Func<PeripheralDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            var item = await _PeripheralRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<Peripheral>(), domainExpressionIncludesList);
            return _mapper.Map<PeripheralDto>(item);
        }

        public async Task<List<PeripheralDto>> FindPageBySpecificationPatternAsync(Specification<PeripheralDto>? specification = null, List<Expression<Func<PeripheralDto, object>>>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            return _mapper.Map<List<PeripheralDto>>(
                await _PeripheralRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<Peripheral>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public PeripheralDto Get(Guid id, List<Expression<Func<PeripheralDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            return _mapper.Map<PeripheralDto>( _PeripheralRepository.Get(id, domainExpressionIncludesList));
        }

        public async Task<List<PeripheralDto>> GetAllAsync(List<Expression<Func<PeripheralDto, object>>>? includes, Dictionary<string, bool>? order)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            var items = await _PeripheralRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<PeripheralDto>>(items.ToList());
            return dtoItems;
        }



        public async Task<PeripheralDto> GetAsync(Guid id, List<Expression<Func<PeripheralDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Peripheral, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
            return _mapper.Map<PeripheralDto>(await _PeripheralRepository.GetAsync(id, domainExpressionIncludesList));
        }


        //public async Task<PeripheralDto> GetForUpdateAsync(Guid id, List<Expression<Func<PeripheralDto, object>>>? includes = null)
        //{
        //    var domainExpressionIncludesList = includes == null
        //        ? new List<Expression<Func<Peripheral, object>>>()
        //        : _mapper.MapIncludesList<Expression<Func<Peripheral, object>>>(includes).ToList();
        //    return _mapper.Map<PeripheralDto>(await _PeripheralRepository.GetAsync(id, domainExpressionIncludesList));
        //}

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _PeripheralRepository.GetAsync(id);
            await _PeripheralRepository.DeleteAsync(item);
            var commited = await _PeripheralRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(PeripheralDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _PeripheralRepository.UpdateAsync(_mapper.Map<Peripheral>(item));
                commited = await _PeripheralRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
