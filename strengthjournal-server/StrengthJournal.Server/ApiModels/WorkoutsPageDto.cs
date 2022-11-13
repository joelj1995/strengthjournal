namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutsPageDto
    {
        public int PerPage { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<WorkoutListDto> Workouts { get; set; }
    }
}
