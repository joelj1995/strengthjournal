namespace StrengthJournal.Server
{
    public sealed class StrengthJournalConfiguration
    {
        private static readonly StrengthJournalConfiguration instance = new StrengthJournalConfiguration();

        public string SqlService_ConnectionString { get; private set; }
        public string Auth0_ClientSecret { get; private set; }
        public string Auth0_ClientId { get; private set; }
        public string Auth0_Audience { get; private set; }
        public string Auth0_BaseURL { get; private set; }

        private StrengthJournalConfiguration()
        {
            SqlService_ConnectionString = @"Server=localhost;Database=StrengthJournal;Trusted_Connection=True";
            Auth0_ClientSecret = "XtCYN0xD2JrDyZN6hFj37qm8aVkyGhJmPipPbS3xcZfaoHmfB3Yb4txgGUsZJq__";
            Auth0_ClientId = "KYRJbp51UUhR91b1B1oGWh3zgbpAmNau";
            Auth0_Audience = "https://localhost:7080/api";
            Auth0_BaseURL = "https://dev-bs65rtlog25jigd0.us.auth0.com/";
        }

        public static StrengthJournalConfiguration Instance
        {
            get => instance;
        }
    }
}
