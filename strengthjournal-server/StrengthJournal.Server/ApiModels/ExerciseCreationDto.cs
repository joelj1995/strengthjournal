using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.ApiModels
{
    public class ExerciseCreationDto
    {
        [MinLength(2)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public Guid? ParentExerciseId { get; set; }
    }
}
