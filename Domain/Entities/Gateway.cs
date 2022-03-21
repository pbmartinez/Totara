using System.Collections.Generic;

namespace Domain.Entities
{
    public class Gateway : Entity
    {
        public Gateway()
        {
            Peripherals = new List<Peripheral>();
        }

        public string Name { get; set; }
        public string Ipv4Address { get; set; }

        public virtual List<Peripheral> Peripherals { get; set; }
        public virtual Brand Brand { get; set; }
    }
}