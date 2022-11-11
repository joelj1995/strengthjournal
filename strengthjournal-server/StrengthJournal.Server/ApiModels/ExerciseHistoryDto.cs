namespace StrengthJournal.Server.ApiModels
{
    public class ExerciseHistoryDto
    {
        public DateTime EntryDateUTC { get; set; }
        public decimal? BodyWeightKg { get; set; }
        public decimal? BodyWeightLbs { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? WeightLbs { get; set; }
        public uint? Reps { get; set; }
        public uint? TargetReps { get; set; }
        public uint? RPE { get; set; }
    }
}
