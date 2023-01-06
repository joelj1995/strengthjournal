using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.MVC.Controllers.API
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
