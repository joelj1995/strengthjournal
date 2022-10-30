using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.ApiModels
{
    public class WorkoutSetSync
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; } = String.Empty;
        public uint? Reps { get; set; }
        public uint? TargetReps { get; set; }
        [Range(0, 2000)]
        public decimal? Weight { get; set; }
        [Required]
        public string WeightUnit { get; set; }
        [Range(0, 20)]
        public uint? RPE { get; set; }
    }
}
