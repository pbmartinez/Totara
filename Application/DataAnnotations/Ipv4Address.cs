using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            // decimal base regular expression pattern
            var regExpression = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            return regExpression.IsMatch(IpAddress);
        }
    }
}
