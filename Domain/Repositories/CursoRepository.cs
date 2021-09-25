using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(IUnitOfWork unitofWork) : base(unitofWork)
        {

        }
    }
}
