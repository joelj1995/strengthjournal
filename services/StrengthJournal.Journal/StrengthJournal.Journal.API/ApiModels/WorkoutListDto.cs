namespace StrengthJournal.Journal.API.ApiModels
{
    public class WorkoutListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EntryDateUTC { get; set; }
        public string Notes { get; set; } = String.Empty;
    }
}
