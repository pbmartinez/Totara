using Domain.Entities;
using Domain.Entities.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Gateway : Entity
    {
        public Gateway()
        {
            Peripherals = new List<Peripheral>();
        }

        public string Name { get; set; } = string.Empty;
        public string Ipv4Address { get; set; } = string.Empty;

        public virtual List<Peripheral> Peripherals { get; set; }
        public virtual Brand? Brand { get; set; }
    }
}