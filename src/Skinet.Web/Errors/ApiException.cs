namespace Skinet.Web.Errors;

public class ApiException : ApiResponse
{
    public ApiException(int code, string msg = null, string details = null) 
        : base(code, msg)
    {
        Details = details;
    }

    public string Details { get; set; }
}
