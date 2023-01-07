using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.IAM.API.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
