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

        [Route("app/assets/{*more}")]
        public ActionResult Index(string more)
        {
            return Redirect($"/dist/assets/{more}");
        }

        [Route("app/{*more}")]
        public IActionResult Index()
        {
            return View(GetAppViewModel());
        }

        protected AppViewModel GetAppViewModel()
        {
            var distFiles = Directory.GetFiles(Path.Combine(this._hostEnvironment.WebRootPath, "dist/")).Select(path => Path.GetFileName(path)).ToList();
            var runtimeFileName = distFiles.FirstOrDefault(f => f.StartsWith("runtime")) ?? throw new Exception("No runtime file in Angular build output");
            var polyfillsFileName = distFiles.FirstOrDefault(f => f.StartsWith("polyfills")) ?? throw new Exception("No polyfills file in Angular build output");
            var mainFileName = distFiles.FirstOrDefault(f => f.StartsWith("main")) ?? throw new Exception("No main file in Angular build output");
            var stylesFileName = distFiles.FirstOrDefault(f => f.StartsWith("styles")) ?? throw new Exception("No main file in Angular build output");
            return new AppViewModel(
                ExtractHashFromAngularDistFile(runtimeFileName),
                ExtractHashFromAngularDistFile(polyfillsFileName),
                ExtractHashFromAngularDistFile(mainFileName),
                ExtractHashFromAngularDistFile(stylesFileName)
            );
        }

        protected string ExtractHashFromAngularDistFile(string fileName)
        {
            return fileName.Split('.').ElementAt(1);
        }
    }
}
