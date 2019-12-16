using System.ComponentModel;

namespace Models
{
    public enum ServiceData
    { 
        [Description("Geo")]
        Geo = 10001,
        [Description("Ping")]
        Ping = 10002,
        [Description("Dns")]
        Dns = 10003,
        [Description("Rdap")]
        Rdap = 10004,
        [Description("All")]
        All = 10005
    }

}
