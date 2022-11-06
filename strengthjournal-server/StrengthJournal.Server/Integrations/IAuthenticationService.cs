using StrengthJournal.Server.Integrations.Models;

namespace StrengthJournal.Server.Integrations
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Authenticate(string username, string password);
    }
}
