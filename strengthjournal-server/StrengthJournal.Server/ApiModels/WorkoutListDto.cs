namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EntryDateUTC { get; set; }
    }
}
