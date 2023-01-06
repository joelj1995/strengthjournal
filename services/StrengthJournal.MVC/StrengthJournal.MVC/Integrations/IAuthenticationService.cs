using StrengthJournal.MVC.Integrations.Models;

namespace StrengthJournal.MVC.Integrations
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Authenticate(string username, string password);
        CreateAccountResponse CreateAccount(string username, string password, bool consentCEM, string countryCode);
        bool ResetPassword(string username);
        bool ResendVerificationEmail(string username);
        bool UpdateEmailAddress(string externalUserId, string newEmail);
    }
}
