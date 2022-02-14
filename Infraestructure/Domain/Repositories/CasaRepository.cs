using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class CasaRepository : Repository<Casa>, ICasaRepository
    {
        public CasaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
