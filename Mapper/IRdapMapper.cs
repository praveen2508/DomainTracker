using DomainModel;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper
{
    public interface IRdapMapper
    {
        RdapInfo Map(RdapData data);
    }
    public class RdapMapper : IRdapMapper
    {
        public RdapInfo Map(RdapData data)
        {
            return null;
        }
    }
}
