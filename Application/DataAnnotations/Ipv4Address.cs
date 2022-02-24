using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.DataAnnotations
{
    public class Ipv4Address : ValidationAttribute
    {
        public Ipv4Address()
        {

        }

        public override bool IsValid(object value)
        {
            var IpAddress = value as string;
            if (string.IsNullOrEmpty(IpAddress) || string.IsNullOrWhiteSpace(IpAddress))
                return false;
            var octects = IpAddress.Split('.');
            if (octects.Length != 4)
                return false;
            var anyNull = octects.Any(o => string.IsNullOrEmpty(o) || string.IsNullOrWhiteSpace(o));
            if(anyNull)
                return false;
            var result = IPAddress.TryParse(IpAddress, out _);
            return result;
        }

    }
}
