using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.Models
{
    public class SignUpModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }
        public bool ConsentCEM { get; set; } = false;
        public string? Error { get; set; }
    }
}
