using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.Journal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("ping")]
        public ActionResult Ping()
        {
            return Ok("journal");
        }
    }
}
