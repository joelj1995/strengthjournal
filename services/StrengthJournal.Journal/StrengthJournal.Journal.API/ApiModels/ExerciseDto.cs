﻿namespace StrengthJournal.Journal.API.ApiModels
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool SystemDefined { get; set; }
        public Guid? ParentExerciseId { get; set; }
    }
}
