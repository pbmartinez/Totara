using Application.Dtos;

namespace Application.IAppServices
{
    public partial interface IEstudianteAppService : IAppService<EstudianteDto, EstudianteDtoForCreate, EstudianteDtoForUpdate, Domain.Entities.Entity>
    {
    }
}
