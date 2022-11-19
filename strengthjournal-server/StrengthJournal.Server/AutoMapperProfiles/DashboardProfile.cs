using AutoMapper;
using StrengthJournal.DataAccess.Model.Virtual;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.AutoMapperProfiles
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<WeeklyVolumeReportLine, WeeklyVolumeReportLineDto>();
            CreateMap<WeeklySBDTonnageReportLine, WeeklyVolumeReportLineDto>();
        }
    }
}
