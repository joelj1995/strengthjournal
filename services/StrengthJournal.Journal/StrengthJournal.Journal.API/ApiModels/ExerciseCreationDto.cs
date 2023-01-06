using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Journal.API.ApiModels
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
