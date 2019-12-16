namespace Models
{
    public class ApiResponse
    {
        public Result Result { get; set; }
        public string WarningMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int? TotalCount { get; set; }
    }
}