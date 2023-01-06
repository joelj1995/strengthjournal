using AutoMapper;
using StrengthJournal.Core.DataAccess.Model.Virtual;
using StrengthJournal.Journal.API.ApiModels;

namespace StrengthJournal.Journal.API.AutoMapperProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ExerciseHistoryLine, ExerciseHistoryDto>();
        }
    }
}
