namespace StrengthJournal.MVC.ApiModels
{
    public class ExerciseHistoryDto
    {
        public DateTime EntryDateUTC { get; set; }
        public decimal? BodyWeightKg { get; set; }
        public decimal? BodyWeightLbs { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? WeightLbs { get; set; }
        public int? Reps { get; set; }
        public int? TargetReps { get; set; }
        public int? RPE { get; set; }
    }
}
