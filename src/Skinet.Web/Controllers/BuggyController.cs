using Microsoft.AspNetCore.Mvc;
using Skinet.Infrastructure.Data;

namespace Skinet.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuggyController : ControllerBase
{
    private readonly StoreContext _db;

    public BuggyController(StoreContext db)
    {
        _db = db;
    }
    
    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var dummy = _db.Products.Find(3737373);
        if (dummy == null)
        {
            return NotFound();
        }
        return Ok();
    }
    
    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
    
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var dummy = _db.Products.Find(3423423);
        var exception = dummy.ToString();
        return Ok();
    }
    
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }
}
