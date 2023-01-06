using AutoMapper;
using StrengthJournal.Core.DataAccess.Model.Virtual;
using StrengthJournal.Journal.API.ApiModels;

namespace StrengthJournal.Journal.API.AutoMapperProfiles
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<WeeklyVolumeReportLine, WeeklyVolumeReportLineDto>();
            CreateMap<WeeklySBDTonnageReportLine, WeeklySBDTonnageReportLineDto>();
        }
    }
}
