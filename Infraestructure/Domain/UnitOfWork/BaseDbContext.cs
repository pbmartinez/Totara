using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Domain.UnitOfWork
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }
    }
}
