using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.Models;

namespace StrengthJournal.Server.Controllers
{
    public class AppController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;

        public AppController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [Route("app-exception")]
        public IActionResult ExceptionHandler([FromQuery] Guid? errorId, [FromQuery] bool showTrace = false)
        {
            var model = new ExceptionHandlerViewModel()
            {
                ErrorId = errorId,
                ShowTrace = showTrace
            };
            return View(model);
        }

    }
}
