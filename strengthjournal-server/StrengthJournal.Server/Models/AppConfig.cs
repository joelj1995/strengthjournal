using Newtonsoft.Json;

namespace StrengthJournal.Server.Models
{
    public class AppConfig
    {
        [JsonProperty("version")]
        public int Version { get => 1; }
        [JsonProperty("preferredWeightUnit")]
        public string PreferredWeightUnit { get; set; } = "lbs";
    }
}
