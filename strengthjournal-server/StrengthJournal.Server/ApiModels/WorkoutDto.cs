namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EntryDateUTC { get; set; }
        public decimal? Bodyweight { get; set; }
        public string BodyweightUnit { get; set; }
        public IEnumerable<WorkoutSetSync> Sets { get; set; }
    }
}
