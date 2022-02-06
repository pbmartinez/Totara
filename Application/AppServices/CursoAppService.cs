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
    public partial class CursoAppService : ICursoAppService
    {
        private readonly IMapper _mapper;
        private readonly ICursoRepository _CursoRepository;

        public CursoAppService(IMapper mapper, ICursoRepository CursoRepository)
        {
            _mapper = mapper;
            _CursoRepository = CursoRepository;
        }

        public async Task<bool> AddAsync(CursoDtoForCreate item)
        {
            await _CursoRepository.AddAsync(_mapper.Map<Domain.Entities.Curso>(item));
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }

        public IEnumerable<CursoDto> FindWithSpecificationPattern(Specification<CursoDto> specification = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CursoDto>> GetAllAsync()
        {
            return _mapper.Map<List<CursoDto>>(await _CursoRepository.GetAllAsync());
        }

        public async Task<CursoDto> GetAsync(Guid id)
        {
            return _mapper.Map<CursoDto>(await _CursoRepository.GetAsync(id));
        }

        public async Task<CursoDtoForUpdate> GetForUpdateAsync(Guid id)
        {
            return _mapper.Map<CursoDtoForUpdate>(await _CursoRepository.GetAsync(id));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _CursoRepository.GetAsync(id);
            await _CursoRepository.DeleteAsync(item);
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(CursoDtoForUpdate item)
        {
            await _CursoRepository.UpdateAsync(_mapper.Map<Domain.Entities.Curso>(item));
            var commited = await _CursoRepository.UnitOfWork.CommitAsync();
            return commited > 0;
        }
    }
}
