using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infrastructure.Domain.Repositories
{
    public partial class PeripheralRepository : Repository<Peripheral>, IPeripheralRepository
    {
        public PeripheralRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
