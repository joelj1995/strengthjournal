using AutoMapper;
using StrengthJournal.DataAccess.Model.Virtual;
using StrengthJournal.MVC.ApiModels;

namespace StrengthJournal.MVC.AutoMapperProfiles
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
