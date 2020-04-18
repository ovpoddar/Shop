using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        [HttpGet]
        public IActionResult Heartbeat()
        {
            return new OkResult();
        }
    }
}