namespace StrengthJournal.MVC.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Error { get; set; }
        public bool IsEmailConfirmed { get; set; } = true;
    }
}
