using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.ApiModels
{
    public class ExerciseUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
