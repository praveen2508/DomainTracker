using DomainModel;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper
{
    public interface IDnsMapper
    {
        DnsInfo Map(DnsData data);
    }
    public class DnsMapper : IDnsMapper
    {
        public DnsInfo Map(DnsData data)
        {
            return new DnsInfo
            {
                HostName = data.HostName
            };
        }
    }
}
