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

        public IEnumerable<MatriculaDto> FindWithSpecificationPattern(Specification<MatriculaDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MatriculaDto>> GetAllAsync()
        {
            return _mapper.Map<List<MatriculaDto>>(await _MatriculaRepository.GetAllAsync());
        }

        public async Task<MatriculaDto> GetAsync(Guid id)
        {
            return _mapper.Map<MatriculaDto>(await _MatriculaRepository.GetAsync(id));
        }

        public async Task<MatriculaDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            return _mapper.Map<MatriculaDtoForUpdate>(await _MatriculaRepository.GetAsync(id));
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
