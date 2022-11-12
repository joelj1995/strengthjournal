using AutoMapper;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.AutoMapperProfiles
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<WorkoutLogEntry, WorkoutListDto>();
        }
    }
}
