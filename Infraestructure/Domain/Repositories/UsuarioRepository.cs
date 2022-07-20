
using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infrastructure.Domain.Repositories
{
    public partial class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
