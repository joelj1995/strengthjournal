using Azure.Core;
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
        private UserService userService;

        public Auth0IdentityService(ILogger<Auth0IdentityService> logger, UserService userService) 
        { 
            this.logger = logger;
            baseUrl = StrengthJournalConfiguration.Instance.Auth0_BaseURL;
            clientId = StrengthJournalConfiguration.Instance.Auth0_ClientId;
            clientSecret = StrengthJournalConfiguration.Instance.Auth0_ClientSecret;
            audience = StrengthJournalConfiguration.Instance.Auth0_Audience;
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
            this.userService = userService;
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

        public async Task<CreateAccountResponse> Register(CreateAccountRequest request)
        {
            try
            {
                var auth0Response = await CreateAuth0Account(request.Username, request.Password);
                var auth0Id = auth0Response.Id;
                var userId = $"auth0|{auth0Id}";
                userService.RegisterUser(request.Username, userId, request.ConsentCEM, request.CountryCode);
                return CreateAccountResponse.Succeed();
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return CreateAccountResponse.Fail(CreateAccountResponse.CreateResult.ValidationError, "There was an error creating your account.");
                else
                    return CreateAccountResponse.Fail(CreateAccountResponse.CreateResult.ServiceFailure, "There was an unexpected error processing your registration.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create account failure");
                return CreateAccountResponse.Fail(CreateAccountResponse.CreateResult.ServiceFailure, "There was an unexpected error processing your registration.");
            }
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                await Auth0ResetPassword(request.Username);
                return new ResetPasswordResponse()
                {
                    Succeeded = true,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Reset password failure");
                return new ResetPasswordResponse()
                {
                    Succeeded = false
                };
            }
        }

        public async Task<SendVerificationResponse> SendVerification(SendVerificationRequest request)
        {
            throw new NotImplementedException();
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
            [JsonPropertyName("email_verified")]
            public bool EmailVerified { get; init; }
        }
        #endregion

        #region RegisterClient

        async Task<Auth0CreateAccountResponse> CreateAuth0Account(string username, string password)
        {
            var request = new RestRequest("dbconnections/signup");
            request.AddParameter("email", username);
            request.AddParameter("password", password);
            request.AddParameter("audience", audience);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("connection", "Username-Password-Authentication");
            return await client.PostAsync<Auth0CreateAccountResponse>(request);
        }

        record Auth0CreateAccountResponse
        {
            [JsonPropertyName("_id")]
            public string Id { get; init; }
        }
        #endregion

        #region ResetPasswordClient
        async Task Auth0ResetPassword(string username)
        {
            var request = new RestRequest("dbconnections/change_password");
            request.AddParameter("email", username);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("connection", "Username-Password-Authentication");
            await client.PostAsync(request);
        }

        #endregion

        #region SendVerificationClient
        async Task Auth0SendVerification(string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ManagementAPIHelpers
        private string GetManagementToken()
        {
            var request = new RestRequest("oauth/token");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("audience", $"{StrengthJournalConfiguration.Instance.Auth0_BaseURL}api/v2/");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            var response = client.Execute(request);
            return ExtractTokenFromResponse(response.Content);
        }

        private string ExtractTokenFromResponse(string response)
        {
            dynamic responseData = JsonConvert.DeserializeObject(response);
            return responseData.access_token;
        }
        #endregion
    }


}
