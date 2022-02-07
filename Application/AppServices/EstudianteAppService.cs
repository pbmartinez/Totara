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

        public Task<IEnumerable<EstudianteDto>> FindWithSpecificationPatternAsync(Specification<EstudianteDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstudianteDto>> GetAllAsync()
        {
            return _mapper.Map<List<EstudianteDto>>(await _EstudianteRepository.GetAllAsync());
        }


        public async Task<List<EstudianteDto>> GetAllAsync(Expression<Func<EstudianteDto, Domain.Entities.Entity>> Includes)
        {
            //convertir la expression de dto a entity
            var domainExpression = _mapper.MapExpression<Expression<Func<Domain.Entities.Estudiante, Domain.Entities.Escuela>>>(Includes);
            var items = await _EstudianteRepository.GetAllAsync(domainExpression);

            //cuando obtenga el resultado de estudiantes domain.entities.estudiante 
            //mapearlo a listado dto.estudiante
            var lis = items.ToList();
            
            var dtoItems = _mapper.Map<List<EstudianteDto>>(lis);
            return dtoItems;
        }

        public async Task<EstudianteDto> GetAsync(Guid id)
        {
            return _mapper.Map<EstudianteDto>(await _EstudianteRepository.GetAsync(id));
        }

        public async Task<EstudianteDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            return _mapper.Map<EstudianteDtoForUpdate>(await _EstudianteRepository.GetAsync(id));
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
