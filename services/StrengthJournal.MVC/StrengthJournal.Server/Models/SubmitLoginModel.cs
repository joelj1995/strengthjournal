using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StrengthJournal.MVC.Models
{
    public class SubmitLoginModel
    {
        public SubmitLoginModel(AppConfig config)
        {
            Token = "";
            UserName = "";
            SuppressRedirect = true;
            Config = config;
        }

        public SubmitLoginModel(
            string token, 
            string userName,
            AppConfig config)
        {
            Token = token;
            UserName = userName;
            SuppressRedirect = false;
            Config = config;
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public bool SuppressRedirect { get; set; }

        public AppConfig Config { get; set; }
        public string ConfigJson { get => JsonConvert.SerializeObject(Config); }
    }
}
