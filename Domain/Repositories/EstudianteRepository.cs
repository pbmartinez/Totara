using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class EstudianteRepository : Repository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
