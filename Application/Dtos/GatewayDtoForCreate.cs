﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class GatewayDtoForCreate : Entity
    {
        public string Name { get; set; }
        public string Ipv4Address { get; set; }
    }
}
