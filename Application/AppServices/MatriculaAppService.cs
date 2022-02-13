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
    public partial class MatriculaAppService : IMatriculaAppService
    {
        private readonly IMapper _mapper;
        private readonly IMatriculaRepository _MatriculaRepository;

        public MatriculaAppService(IMapper mapper, IMatriculaRepository MatriculaRepository)
        {
            _mapper = mapper;
            _MatriculaRepository = MatriculaRepository;
        }

        public async Task<bool> AddAsync(MatriculaDtoForCreate item)
        {
            await _MatriculaRepository.AddAsync(_mapper.Map<Domain.Entities.Matricula>(item));
            var commited = await _MatriculaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<MatriculaDto>> FindAllBySpecificationPatternAsync(Specification<MatriculaDto> specification = null, List<Expression<Func<MatriculaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(includes).ToList();
            return _mapper.Map<List<MatriculaDto>>(
                await _MatriculaRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Matricula, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<MatriculaDto> specification = null)
        {
            var count = await _MatriculaRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Domain.Entities.Matricula>());
            return count;
        }

        public async Task<MatriculaDto> FindOneBySpecificationPatternAsync(Specification<MatriculaDto> specification = null, List<Expression<Func<MatriculaDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(includes).ToList();
            var item = await _MatriculaRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Domain.Entities.Matricula>(), domainExpressionIncludesList);
            return _mapper.Map<MatriculaDto>(item);
        }

        public async Task<List<MatriculaDto>> FindPageBySpecificationPatternAsync(Specification<MatriculaDto> specification = null, List<Expression<Func<MatriculaDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(includes).ToList();
            return _mapper.Map<List<MatriculaDto>>(
                await _MatriculaRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Domain.Entities.Matricula>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }

        public async Task<List<MatriculaDto>> GetAllAsync(List<Expression<Func<MatriculaDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(includes).ToList();
            var items = await _MatriculaRepository.GetAllAsync(domainExpressionIncludesList);
            var dtoItems = _mapper.Map<List<MatriculaDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<MatriculaDto> GetAsync(Guid id, List<Expression<Func<MatriculaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(Includes).ToList();
            return _mapper.Map<MatriculaDto>(await _MatriculaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<MatriculaDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<MatriculaDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Matricula, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Matricula, object>>>(Includes).ToList();
            return _mapper.Map<MatriculaDtoForUpdate>(await _MatriculaRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _MatriculaRepository.GetAsync(id);
            await _MatriculaRepository.DeleteAsync(item);
            var commited = await _MatriculaRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(MatriculaDtoForUpdate item)
        {
            await _MatriculaRepository.UpdateAsync(_mapper.Map<Domain.Entities.Matricula>(item));
            var commited = await _MatriculaRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
