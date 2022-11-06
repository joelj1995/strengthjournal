namespace StrengthJournal.Server.Models
{
    public class SubmitLoginModel
    {
        public SubmitLoginModel(string token, string userName, bool suppressRedirect = false)
        {
            Token = token;
            UserName = userName;
            SuppressRedirect = suppressRedirect;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public bool SuppressRedirect { get; set; }
    }
}
