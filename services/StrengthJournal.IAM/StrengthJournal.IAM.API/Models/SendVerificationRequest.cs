using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.IAM.API.Models
{
    public class SendVerificationRequest
    {
        [Required]
        public string Username { get; set; }
    }
}
