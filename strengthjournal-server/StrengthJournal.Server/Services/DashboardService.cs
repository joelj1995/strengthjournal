using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.Services
{
    public class DashboardService
    {
        protected readonly StrengthJournalContext context;
        protected readonly IMapper mapper;

        public DashboardService(StrengthJournalContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WeeklyVolumeReportLineDto>> GetWeeklyVolumeReport(Guid userId)
        {
            return context.WeeklyVolumeReportLines
                .FromSqlRaw("EXEC spGenerateWeeklyVolumeReport @userId", new SqlParameter("userId", userId))
                .AsEnumerable()
                .Select(line => mapper.Map<WeeklyVolumeReportLineDto>(line));
        }

        public async Task<IEnumerable<WeeklySBDTonnageReportLineDto>> GetWeeklySBDTonnageReport(Guid userId)
        {
            return context.WeeklySBDTonnageReportLines
                .FromSqlRaw("EXEC spGenerateWeeklySBDTonnageReport @userId", new SqlParameter("userId", userId))
                .AsEnumerable()
                .Select(line => mapper.Map<WeeklySBDTonnageReportLineDto>(line));
        }
    }
}
