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
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("weekly-volume-report")]
        public async Task<IEnumerable<WeeklyVolumeReportLineDto>> GetWeeklyVolumeReport()
        {
            var userId = HttpContext.GetUserId();
            return await dashboardService.GetWeeklyVolumeReport(userId);
        }
    }
}
