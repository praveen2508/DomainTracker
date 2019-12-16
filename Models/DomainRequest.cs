using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class DomainRequest
    {
        public string IpAddress { get; set; }
        public ServiceData services { set;get; }
        // public IList<String> ServicesUrl { get; set; }
    }
     public class DomainValidator : AbstractValidator<DomainRequest>
    {
        public DomainValidator()
        {
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("Ip Address can't be empty");
            When(x => !string.IsNullOrWhiteSpace(x.IpAddress),
                () =>
                {
                    RuleFor(x => x.IpAddress).Must(IsValidateIpAddress).WithMessage("Invalid Ip address");
                });
        }

        private bool IsValidateIpAddress(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            string[] splitValues = ipAddress.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
