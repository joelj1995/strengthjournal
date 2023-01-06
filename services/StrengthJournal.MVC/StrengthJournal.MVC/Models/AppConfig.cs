using Newtonsoft.Json;

namespace StrengthJournal.MVC.Models
{
    public class AppConfig
    {
        public int version { get => 2; }
        public string preferredWeightUnit { get; set; } = "lbs";
        public ICollection<string> features { get; set; } = new List<string>();
    }
}
