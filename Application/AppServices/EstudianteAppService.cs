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

        public async Task<IEnumerable<EstudianteDto>> FindWithSpecificationPatternAsync(Specification<EstudianteDto> specification = null)
        {
            return _mapper.Map<List<EstudianteDto>>(
                await _EstudianteRepository.FindWithExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Domain.Entities.Estudiante, bool>>>(
                        specification == null ? a => true : specification.ToExpression())));
        }

        public async Task<List<EstudianteDto>> GetAllAsync(List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(Includes).ToList();
            var items = await _EstudianteRepository.GetAllAsync(domainExpressionList);
            var dtoItems = _mapper.Map<List<EstudianteDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<EstudianteDto> GetAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(Includes).ToList();
            return _mapper.Map<EstudianteDto>(await _EstudianteRepository.GetAsync(id, domainExpressionList));
        }

        public async Task<EstudianteDtoForUpdate> GetForUpdateAsync(Guid id, List<Expression<Func<EstudianteDto, object>>> Includes = null)
        {
            var domainExpressionList = Includes == null
                ? new List<Expression<Func<Domain.Entities.Estudiante, object>>>()
                : _mapper.MapIncludesList<Expression<Func<Domain.Entities.Estudiante, object>>>(Includes).ToList();
            return _mapper.Map<EstudianteDtoForUpdate>(await _EstudianteRepository.GetAsync(id, domainExpressionList));
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
