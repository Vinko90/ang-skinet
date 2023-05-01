namespace Skinet.Web.Errors;

public class ApiResponse
{
    public ApiResponse(int code, string msg = null)
    {
        StatusCode = code;
        Message = msg ?? DefaultMsgForStatusCode(code);
    }
    
    public int StatusCode { get; set; }
    public string Message { get; set; }

    private string DefaultMsgForStatusCode(int code)
    {
        return code switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Errors are the path to the dark side",
            _ => null
        };
    }
}
