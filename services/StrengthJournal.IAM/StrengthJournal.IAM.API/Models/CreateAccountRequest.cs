using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.IAM.API.Models
{
    public class CreateAccountRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool ConsentCEM { get; set; }
        [Required]
        public string CountryCode { get; set; }
    }
}
