using StrengthJournal.Core.Integrations.Models;

namespace StrengthJournal.Core.Integrations
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Authenticate(string username, string password);
        CreateAccountResponse CreateAccount(string username, string password, bool consentCEM, string countryCode);
        bool ResetPassword(string username);
        bool ResendVerificationEmail(string username);
    }
}
