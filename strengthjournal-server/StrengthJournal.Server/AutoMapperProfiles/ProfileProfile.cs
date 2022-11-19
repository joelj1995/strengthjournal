﻿using AutoMapper;
using StrengthJournal.DataAccess.Model;
using StrengthJournal.Server.ApiModels;

namespace StrengthJournal.Server.AutoMapperProfiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
