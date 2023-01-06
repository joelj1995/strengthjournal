namespace StrengthJournal.Core.Integrations.Models
{
    public class AuthenticationResponse
    {
        public enum AuthResult
        {
            Success,
            WrongPassword,
            ServiceFailure,
            EmailNotVerified
        }

        public AuthResult Result;
        public string? Token;
        public dynamic? Profile { get; set; }
    }
}
