using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class IpResponse :ApiResponse
    {
        public GeoIpInfo GeoData { get; set; }
        public PingInfo PingInfo { get; set; }
        public RdapInfo RdapInfo { get; set; }
        public DnsInfo DnsInfo { get; set; }
    }

}
