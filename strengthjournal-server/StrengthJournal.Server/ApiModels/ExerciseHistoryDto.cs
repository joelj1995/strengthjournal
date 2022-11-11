namespace StrengthJournal.Server.ApiModels
{
    public class ExerciseHistoryDto
    {
        public DateTime EntryDateUTC { get; set; }
        public uint? Reps { get; set; }
        public uint? TargetReps { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; }
        public uint? RPE { get; set; }
    }
}
