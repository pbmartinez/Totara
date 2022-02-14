using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Domain.Repositories
{
    public partial class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
