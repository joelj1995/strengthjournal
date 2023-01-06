﻿using AutoMapper;
using StrengthJournal.DataAccess.Model.Virtual;
using StrengthJournal.MVC.ApiModels;

namespace StrengthJournal.MVC.AutoMapperProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ExerciseHistoryLine, ExerciseHistoryDto>();
        }
    }
}