﻿using Azure.Core;
using Newtonsoft.Json;
using RestSharp;
using StrengthJournal.Core;
using StrengthJournal.IAM.API.Models;
using System.Text.Json.Serialization;

namespace StrengthJournal.IAM.API.Services.Implementation
{
    public class Auth0IdentityService : IIdentityService
    {
        private ILogger<Auth0IdentityService> logger;
        private readonly string baseUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string audience;
        private readonly RestClient client;

        public Auth0IdentityService(ILogger<Auth0IdentityService> logger) 
        { 
            this.logger = logger;
            baseUrl = StrengthJournalConfiguration.Instance.Auth0_BaseURL;
            clientId = StrengthJournalConfiguration.Instance.Auth0_ClientId;
            clientSecret = StrengthJournalConfiguration.Instance.Auth0_ClientSecret;
            audience = StrengthJournalConfiguration.Instance.Auth0_Audience;
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            try
            {
                var tokenResponse = await GetLoginToken(request.UserName, request.Password);
                var profileResponse = await GetProfileData(tokenResponse.AccessToken);
                if (!profileResponse.EmailVerified)
                    return LoginResponse.Fail(LoginResponse.AuthResult.EmailNotVerified);
                return LoginResponse.Succeed(tokenResponse.AccessToken);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return LoginResponse.Fail(LoginResponse.AuthResult.WrongPassword);
                else
                    return LoginResponse.Fail(LoginResponse.AuthResult.ServiceFailure);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Authentication failure");
                return LoginResponse.Fail(LoginResponse.AuthResult.ServiceFailure);
            }
        }

        #region AuthenticationClient
        async Task<AuthenticateTokenResponse> GetLoginToken(string userName, string password)
        {
            var tokenRequest = new RestRequest("oauth/token");
            tokenRequest.AddParameter("grant_type", "password");
            tokenRequest.AddParameter("scope", "openid");
            tokenRequest.AddParameter("username", userName);
            tokenRequest.AddParameter("password", password);
            tokenRequest.AddParameter("audience", audience);
            tokenRequest.AddParameter("client_id", clientId);
            tokenRequest.AddParameter("client_secret", clientSecret);
            return await client.PostAsync<AuthenticateTokenResponse>(tokenRequest);
        }

        async Task<ProfileDataResponse> GetProfileData(string token)
        {
            var profileDataRequest = new RestRequest("userinfo")
                .AddHeader("Authorization", $"Bearer {token}");
            return await client.GetAsync<ProfileDataResponse>(profileDataRequest);
        }
        

        record AuthenticateTokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; init; }
            [JsonPropertyName("scope")]
            public string Scope { get; init; }
            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; init; }
            [JsonPropertyName("token_type")]
            public string TokenType { get; init; }
        }

        record ProfileDataResponse
        {
            [JsonProperty("email_verified")]
            public bool EmailVerified { get; init; }
        }
        #endregion
    }


}
