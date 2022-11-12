﻿using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace StrengthJournal.Server
{
    public sealed class StrengthJournalConfiguration
    {
        private readonly IConfiguration _configuration;
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

        public string GetSecret(string secretName)
        {
            if (_configuration["AzKeyVaultEndpoint"] != null)
            {
                var client = new SecretClient(vaultUri: new Uri(_configuration["AzKeyVaultEndpoint"]), credential: new DefaultAzureCredential());
                KeyVaultSecret secret = client.GetSecret(secretName);
                return secret.Value;
            }
            else
            {
                return _configuration[$"Secrets:{secretName}"];
            }
        }

        private StrengthJournalConfiguration(IConfiguration configuration)
        {
            /*
             * Get non-senstive configuration
             */
            _configuration = configuration;
            Auth0_Audience = configuration["Auth0:Audience"];
            Auth0_BaseURL = configuration["Auth0:BaseURL"];
            /*
             * Get secrets
             */
            TestSecret = GetSecret("TestSecret");
            SqlServer_ConnectionString = GetSecret("SqlServer-ConnectionString");
            Auth0_ClientSecret = GetSecret("Auth0-ClientSecret");
            Auth0_ClientId = GetSecret("Auth0-ClientId");
        }

        public static StrengthJournalConfiguration Instance
        {
            get => instance ?? throw new NullReferenceException("StrengthJournalConfiguration configuration is null. Did you run Init?");
        }
    }
}