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
    public partial class CasaAppService : ICasaAppService
    {
        private readonly ICasaRepository _CasaRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;
        public CasaAppService(ICasaRepository CasaRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _CasaRepository = CasaRepository;
            _mapper = mapper;
            _entityValidator = entityValidator;
        }
        public async Task<bool> AddAsync(CasaDtoForCreate item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _CasaRepository.AddAsync(_mapper.Map<Casa>(item));
                commited = await _CasaRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

        public async Task<List<CasaDto>> FindAllBySpecificationPatternAsync(Specification<CasaDto>? specification = null, List<Expression<Func<CasaDto, object>>>? includes = null, Dictionary<string, bool>? order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<List<CasaDto>>(
                await _CasaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Casa, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<CasaDto>? specification = null)
        {
            var count = await _CasaRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<Casa>());
            return count;
        }

        public async Task<CasaDto> FindOneBySpecificationPatternAsync(Specification<CasaDto>? specification = null, List<Expression<Func<CasaDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            var item = await _CasaRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<Casa>(), domainExpressionIncludesList);
            return _mapper.Map<CasaDto>(item);
        }

        public async Task<List<CasaDto>> FindPageBySpecificationPatternAsync(Specification<CasaDto>? specification = null, List<Expression<Func<CasaDto, object>>>? includes = null, Dictionary<string, bool>? order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<List<CasaDto>>(
                await _CasaRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<Casa>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public async Task<List<CasaDto>> GetAllAsync(List<Expression<Func<CasaDto, object>>>? includes, Dictionary<string, bool>? order)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            var items = await _CasaRepository.GetAllAsync(domainExpressionIncludesList, order);
            var dtoItems = _mapper.Map<List<CasaDto>>(items.ToList());
            return dtoItems;
        }



        public async Task<CasaDto> GetAsync(Guid id, List<Expression<Func<CasaDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<CasaDto>(await _CasaRepository.GetAsync(id, domainExpressionIncludesList));
        }


        public async Task<CasaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<CasaDto, object>>>? includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Casa, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Casa, object>>>(includes).ToList();
            return _mapper.Map<CasaDtoForUpdate>(await _CasaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _CasaRepository.GetAsync(id);
            await _CasaRepository.DeleteAsync(item);
            var commited = await _CasaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(CasaDtoForUpdate item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _CasaRepository.UpdateAsync(_mapper.Map<Casa>(item));
                commited = await _CasaRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
