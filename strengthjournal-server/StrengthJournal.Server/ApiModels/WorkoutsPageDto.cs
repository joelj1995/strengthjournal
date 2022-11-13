namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutsPageDto
    {
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<WorkoutListDto> Workouts { get; set; }
    }
}
