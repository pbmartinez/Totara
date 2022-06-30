using Domain.Entities;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand : Entity
    {
        public string Name { get; set; }
        public string Sponsor { get; set; }

        public Guid GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }
    }
}
