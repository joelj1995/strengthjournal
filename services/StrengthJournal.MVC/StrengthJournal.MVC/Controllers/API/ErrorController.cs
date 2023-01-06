using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.MVC.ApiModels;
using StrengthJournal.MVC.Extensions;
using StrengthJournal.MVC.Services;

namespace StrengthJournal.MVC.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ErrorService errorService;

        public ErrorController(ErrorService errorService)
        {
            this.errorService = errorService;
        }

        [HttpPost]
        public async Task<ActionResult> LogError(AppErrorDto appError)
        {
            var userId = HttpContext.GetUserId();
            await errorService.LogError(appError.Id, userId, appError.ErrorMessage ?? "", appError.ApiTraceId);
            return Ok();
        }
    }
}
