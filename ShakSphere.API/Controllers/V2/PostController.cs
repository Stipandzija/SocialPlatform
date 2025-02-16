namespace ShakSphere.API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet("Hello")]
        public IActionResult GetByID()
        {
            return Ok("Hello from V2");
        }
    }
}
