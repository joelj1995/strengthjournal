namespace StrengthJournal.Core.Integrations.Models
{
    public class CreateAccountResponse
    {
        public enum CreateResult
        {
            Success,
            ValidationError,
            ServiceFailure
        }

        public CreateResult Result;
        public string ErrorMessage = "";
    }
}
