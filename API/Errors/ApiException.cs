namespace API.Errors
{
    public class ApiException
    {
        public string Message { get; set; }
        public string Details { get; }
        public int StatusCode { get; set; }
        public ApiException(int statusCode, string message = null, string details = null)
        {
            Details = details;
            Message = message;
            StatusCode = statusCode;
        }

        
    }
}