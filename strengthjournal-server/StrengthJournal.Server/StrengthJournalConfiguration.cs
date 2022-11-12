using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace StrengthJournal.Server
{
    public sealed class StrengthJournalConfiguration
    {
        private static StrengthJournalConfiguration? instance;

        public string SqlServer_ConnectionString { get; private set; }
        public string Auth0_ClientSecret { get; private set; }
        public string Auth0_ClientId { get; private set; }
        public string Auth0_Audience { get; private set; }
        public string Auth0_BaseURL { get; private set; }
        public string TestSecret { get; private set; } = "Not Set";

        public static void Init(IConfiguration configuration)
        {
            if (instance != null)
                throw new Exception("The configuration has already been initialized.");
            instance = new StrengthJournalConfiguration(configuration);
        }

        public string GetSecret(SecretClient client, string secretName)
        {
            KeyVaultSecret secret = client.GetSecret(secretName);
            return secret.Value;
        }

        private StrengthJournalConfiguration(IConfiguration configuration)
        {
            Auth0_Audience = configuration["Auth0:Audience"];
            Auth0_BaseURL = configuration["Auth0:BaseURL"];
            if (configuration["AzKeyVaultEndpoint"] != null)
            {
                var client = new SecretClient(vaultUri: new Uri(configuration["AzKeyVaultEndpoint"]), credential: new DefaultAzureCredential());
                TestSecret = GetSecret(client, "TestSecret");
                SqlServer_ConnectionString = GetSecret(client, "SqlServer-ConnectionString");
            }
            SqlServer_ConnectionString = @"Server=localhost;Database=StrengthJournal;Trusted_Connection=True";
            Auth0_ClientSecret = "XtCYN0xD2JrDyZN6hFj37qm8aVkyGhJmPipPbS3xcZfaoHmfB3Yb4txgGUsZJq__";
            Auth0_ClientId = "KYRJbp51UUhR91b1B1oGWh3zgbpAmNau";
        }

        public static StrengthJournalConfiguration Instance
        {
            get => instance ?? throw new NullReferenceException("StrengthJournalConfiguration configuration is null. Did you run Init?");
        }
    }
}
