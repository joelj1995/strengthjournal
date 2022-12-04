using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutCreationDto
    {
        public string Title { get; set; }
        public DateTime EntryDateUTC { get; set; }
        [Range(0, 1000)]
        public uint? Bodyweight { get; set; }
        public string BodyweightUnit { get; set; }
    }
}
