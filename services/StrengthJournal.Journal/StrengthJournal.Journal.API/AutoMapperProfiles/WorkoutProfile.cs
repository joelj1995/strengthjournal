using AutoMapper;
using StrengthJournal.Core.DataAccess.Model;
using StrengthJournal.Journal.API.ApiModels;

namespace StrengthJournal.Journal.API.AutoMapperProfiles
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
