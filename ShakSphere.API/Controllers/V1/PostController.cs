using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ShakSphere.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet("Hello")]
        public IActionResult GetByID()
        {
            return Ok("Hello from V1");
        }
    }
}
