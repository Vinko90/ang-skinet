using Microsoft.AspNetCore.Mvc;
using Skinet.Web.Errors;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("errors/{code:int}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}
