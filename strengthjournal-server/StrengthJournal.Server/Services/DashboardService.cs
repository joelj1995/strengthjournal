using AutoMapper;
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
            return await context.WeeklyVolumeReportLines
                .FromSqlRaw("EXEC spGenerateWeeklyVolumeReport @userId", userId)
                .Select(line => mapper.Map<WeeklyVolumeReportLineDto>(line))
                .ToListAsync();
        }
    }
}
