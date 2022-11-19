namespace StrengthJournal.Server.ApiModels
{
    public class WeeklySBDTonnageReportLineDto
    {
        public DateTime WeekStart { get; set; }
        public int SquatTonnage { get; set; }
        public int BenchTonnage { get; set; }
        public int DeadliftTonnage { get; set; }
    }
}
