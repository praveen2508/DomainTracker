using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Models
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
    }
}
