namespace StrengthJournal.Server.Models
{
    public class AppConfig
    {
        public int Version { get => 1; }
        public string PreferredWeightUnit { get; set; } = "lbs";
    }
}
