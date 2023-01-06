using AutoMapper;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.MVC.ApiModels;

namespace StrengthJournal.MVC.AutoMapperProfiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
