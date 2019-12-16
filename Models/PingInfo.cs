using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PingInfo
    {
        public string Address { get; set; }
        public long RoundtripTime { get; set; }
        public int TTL { get; set; }
        public bool DontFragment { get; set; }
        public int BufferLength { get; set; }
        public string Status { get; set; }
    }
}
