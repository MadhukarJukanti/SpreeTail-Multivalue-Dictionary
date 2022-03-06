
using Microsoft.AspNetCore.Mvc;

namespace SpreeTail.MultiValueDictionary.User.Api.Controllers
{
    [Route("api/v1/health")]
    public class HealthController : ControllerBase
    {

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}
