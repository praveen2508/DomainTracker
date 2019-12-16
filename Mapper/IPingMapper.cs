using DomainModel;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper
{
    public interface IPingMapper
    {
        PingInfo Map(PingData data);
    }

    public class PingMapper : IPingMapper
    {
        public PingInfo Map(PingData data)
        {
            return new PingInfo
            {
                Address = data.Address,
                TTL = data.TTL,
                RoundtripTime = data.RoundtripTime,
                DontFragment = data.DontFragment,
                BufferLength = data.BufferLength,
                Status = data.Status
            };
        }
    }
}
