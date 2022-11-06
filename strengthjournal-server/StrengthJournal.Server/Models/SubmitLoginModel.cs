namespace StrengthJournal.Server.Models
{
    public class SubmitLoginModel
    {
        public SubmitLoginModel(string token, string userName)
        {
            Token = token;
            UserName = userName;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
