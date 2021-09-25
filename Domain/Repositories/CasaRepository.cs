using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class CasaRepository : Repository<Casa>, ICasaRepository
    {
        public CasaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
