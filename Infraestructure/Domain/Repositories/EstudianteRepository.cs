using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class EstudianteRepository : Repository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
