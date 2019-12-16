using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Models
{
    [ExcludeFromCodeCoverage]
    public class ResultDetail
    {
        public string Detail { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Result
    {
        public string ResultCode { get; set; }
        public List<ResultDetail> ResultDetailList { get; set; }
    }
}
