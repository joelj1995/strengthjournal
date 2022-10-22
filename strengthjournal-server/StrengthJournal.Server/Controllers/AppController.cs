using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.Server.Controllers
{
    public class AppController : Controller
    {
        [Route("app/{*more}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
