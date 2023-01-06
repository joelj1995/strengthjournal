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

        [HttpGet("weekly-tonnage-report")]
        public async Task<IEnumerable<WeeklySBDTonnageReportLineDto>> GetWeeklyTonnageReport()
        {
            var userId = HttpContext.GetUserId();
            return await dashboardService.GetWeeklySBDTonnageReport(userId);
        }
    }
}
