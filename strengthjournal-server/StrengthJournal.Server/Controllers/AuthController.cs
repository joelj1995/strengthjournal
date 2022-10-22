using Microsoft.AspNetCore.Mvc;

namespace StrengthJournal.Server.Controllers
{
    public class AuthController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
