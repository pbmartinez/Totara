using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class PeripheralDto : Entity
    {
        public string Vendor { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public Guid GatewayId { get; set; }
    }
}
