using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class PingData
    {
        public string Address { get; set; }
        public long RoundtripTime { get; set; }
        public int TTL { get; set; }
        public bool DontFragment { get; set; }
        public int BufferLength { get; set; }
        public string Status { get; set; }
    }
}
