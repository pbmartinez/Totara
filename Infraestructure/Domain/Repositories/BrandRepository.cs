
using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
