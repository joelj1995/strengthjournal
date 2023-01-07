using StrengthJournal.Core.Integrations.Models;

namespace StrengthJournal.IAM.API.Models
{
    public class CreateAccountResponse
    {
        private CreateAccountResponse() { }

        public enum CreateResult
        {
            Success,
            ValidationError,
            ServiceFailure
        }

        public CreateResult ResultCode;
        public string Result { get => ResultCode.ToString(); }
        public string ErrorMessage = "";

        public static CreateAccountResponse Fail(CreateResult reason, string ErrorMessage)
        {
            if (reason == CreateResult.Success)
                throw new InvalidOperationException("Cannot create failed response with success result");
            return new CreateAccountResponse
            {
                ResultCode = reason,
                ErrorMessage = ErrorMessage
            };
        }

        public static CreateAccountResponse Succeed()
        {
            return new CreateAccountResponse
            {
                ResultCode = CreateResult.Success,
            };
        }
    }
}
