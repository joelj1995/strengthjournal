using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.MVC.ApiControllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("ping")]
        public ActionResult Ping()
        {
            return Ok("mvc");
        }

        [Route("environment")]
        public ActionResult GetEnvironment()
        {
            var environmentName = Environment.GetEnvironmentVariable("AZ_ENVIRONMENT");
            return Ok(environmentName ?? "N/A");
        }
    }
}
