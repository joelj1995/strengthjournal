namespace StrengthJournal.IAM.API.Models
{
    

    public class LoginResponse
    {
        public enum AuthResult
        {
            Success,
            WrongPassword,
            ServiceFailure,
            EmailNotVerified
        }

        public AuthResult ResultCode;
        public string Result { get => ResultCode.ToString(); }
        public string? Token { get; set; }

        public static LoginResponse Fail(AuthResult reason)
        {
            if (reason == AuthResult.Success)
                throw new InvalidOperationException("Cannot create a failed response with success reason code");
            return new LoginResponse
            {
                ResultCode = reason
            };
        }

        public static LoginResponse Succeed(string token)
        {
            return new LoginResponse { Token = token, ResultCode = AuthResult.Success };
        }
    }
}