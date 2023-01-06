using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Journal.API.ApiModels
{
    public class WorkoutSetSync
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; } = String.Empty;
        [Range(0, 2000)]
        public int? Reps { get; set; }
        [Range(0, 2000)]
        public int? TargetReps { get; set; }
        [Range(0, 2000)]
        public decimal? Weight { get; set; }
        [Required]
        public string WeightUnit { get; set; }
        [Range(0, 20)]
        public int? RPE { get; set; }
    }
}
