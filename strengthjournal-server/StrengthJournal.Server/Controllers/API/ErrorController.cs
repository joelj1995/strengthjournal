using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
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
