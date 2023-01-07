using StrengthJournal.IAM.API.Models;

namespace StrengthJournal.IAM.API.Services
{
    public interface IIdentityService
    {
        public Task<LoginResponse> Authenticate(LoginRequest request);
    }
}
