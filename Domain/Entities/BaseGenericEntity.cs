using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseGenericEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public abstract bool IsTransient();

        public abstract void GenerateIdentity();
    }
}
