namespace StrengthJournal.Server.Integrations.Models
{
    public class AuthenticationResponse
    {
        public enum AuthResult
        {
            Success,
            WrongPassword,
            ServiceFailure
        }

        public AuthResult Result;
        public string? Token;
    }
}
