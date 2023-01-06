using AutoMapper;
using StrengthJournal.Core.DataAccess.Model;
using StrengthJournal.Journal.API.ApiModels;

namespace StrengthJournal.Journal.API.AutoMapperProfiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
