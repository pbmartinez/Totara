using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class MatriculaRepository : Repository<Matricula>, IMatriculaRepository
    {
        public MatriculaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
