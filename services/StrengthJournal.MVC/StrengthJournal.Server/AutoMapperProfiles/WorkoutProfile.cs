using AutoMapper;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.MVC.ApiModels;

namespace StrengthJournal.MVC.AutoMapperProfiles
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutLogEntry, WorkoutListDto>();
            CreateMap<WorkoutLogEntrySet, WorkoutSetSync>()
                .ForMember(
                    dest => dest.WeightUnit,
                    opt => opt.MapFrom(src => src.WeightUnitAbbreviation));
        }
    }
}
