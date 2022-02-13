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
    public partial class EstudianteAppService : IEstudianteAppService
    {
        private readonly IMapper _mapper;
        private readonly IEstudianteRepository _EstudianteRepository;

        public EstudianteAppService(IMapper mapper, IEstudianteRepository EstudianteRepository)
        {
            _mapper = mapper;
            _EstudianteRepository = EstudianteRepository;
        }

        public async Task<bool> AddAsync(EstudianteDtoForCreate item)
        {
            await _EstudianteRepository.AddAsync(_mapper.Map<Domain.Entities.Estudiante>(item));
            var commited = await _EstudianteRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public async Task<List<EstudianteDto>> FindAllBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
               ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
               : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(includes).ToList();
            return _mapper.Map<List<EstudianteDto>>(
                await _EstudianteRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Estudiante, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), domainExpressionIncludesList, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<EstudianteDto> specification = null)
        {
            var count = await _EstudianteRepository.FindCountByExpressionAsync(specification.MapToExpressionOfType<Domain.Entities.Estudiante>());
            return count;
        }

        public async Task<EstudianteDto> FindOneBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(includes).ToList();
            var item = await _EstudianteRepository.FindOneByExpressionAsync(specification.MapToExpressionOfType<Domain.Entities.Estudiante>(), domainExpressionIncludesList);
            return _mapper.Map<EstudianteDto>(item);
        }

        public async Task<List<EstudianteDto>> FindPageBySpecificationPatternAsync(Specification<EstudianteDto> specification = null, List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(includes).ToList();
            return _mapper.Map<List<EstudianteDto>>(
                await _EstudianteRepository.FindPageByExpressionAsync(
                    specification.MapToExpressionOfType<Domain.Entities.Estudiante>(), domainExpressionIncludesList, order, pageSize, pageGo));
        }



        public async Task<List<EstudianteDto>> GetAllAsync(List<Expression<Func<EstudianteDto, object>>> includes = null, Dictionary<string, bool> order = null)
        {
            var domainExpressionIncludesList = includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(includes).ToList();
            var items = await _EstudianteRepository.GetAllAsync(domainExpressionIncludesList);
            var dtoItems = _mapper.Map<List<EstudianteDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<EstudianteDto> GetAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(Includes).ToList();
            return _mapper.Map<EstudianteDto>(await _EstudianteRepository.GetAsync(id, domainExpressionIncludesList));
        }

        public async Task<EstudianteDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionIncludesList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(Includes).ToList();
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
            await _EstudianteRepository.UpdateAsync(_mapper.Map<Domain.Entities.Estudiante>(item));
            var commited = await _EstudianteRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
