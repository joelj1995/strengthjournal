using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.IAM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("ping")]
        public ActionResult Ping()
        {
            return Ok("iam");
        }
    }
}
