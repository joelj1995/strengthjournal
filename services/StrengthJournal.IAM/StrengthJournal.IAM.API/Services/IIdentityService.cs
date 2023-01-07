using StrengthJournal.IAM.API.Models;

namespace StrengthJournal.IAM.API.Services
{
    public interface IIdentityService
    {
        Task<LoginResponse> Authenticate(LoginRequest request);
        Task<CreateAccountResponse> Register(CreateAccountRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
    }
}
