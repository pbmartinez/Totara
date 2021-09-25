using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using Domain.IRepositories;
using Domain.Specification;
using System;
using System.Collections.Generic;
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

        public IEnumerable<EstudianteDto> FindWithSpecificationPattern(IExpressionSpecification<EstudianteDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstudianteDto>> GetAllAsync()
        {
            return _mapper.Map<List<EstudianteDto>>(await _EstudianteRepository.GetAllAsync());
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
