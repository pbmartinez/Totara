using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
