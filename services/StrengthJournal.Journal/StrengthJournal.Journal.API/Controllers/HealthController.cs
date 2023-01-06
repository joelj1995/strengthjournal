using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.Journal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("ping")]
        public ActionResult Ping()
        {
            return Ok("pong");
        }
    }
}
