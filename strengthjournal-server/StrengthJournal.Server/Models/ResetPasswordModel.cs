namespace StrengthJournal.Server.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
