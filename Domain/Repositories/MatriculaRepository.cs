using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class MatriculaRepository : Repository<Matricula>, IMatriculaRepository
    {
        public MatriculaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
