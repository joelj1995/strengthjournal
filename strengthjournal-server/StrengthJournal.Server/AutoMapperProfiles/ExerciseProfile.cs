using AutoMapper;
using StrengthJournal.DataAccess.Model.Virtual;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.AutoMapperProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ExerciseHistoryLine, ExerciseHistoryDto>();
        }
    }
}
