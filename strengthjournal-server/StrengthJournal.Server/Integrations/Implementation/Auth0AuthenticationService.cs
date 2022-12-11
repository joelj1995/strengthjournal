using Newtonsoft.Json;
using RestSharp;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.Integrations.Models;
using StrengthJournal.Server.Services;
using System.Net;

namespace StrengthJournal.Server.Integrations.Implementation
{
    public class Auth0AuthenticationService : IAuthenticationService
    {
        private readonly StrengthJournalContext context;
        private readonly UserService userService;
        private readonly RestClient client;
        private readonly ILogger<Auth0AuthenticationService> logger;

        private readonly string clientSecret;
        private readonly string clientId;
        private readonly string audience;
        private readonly string connection;

        public Auth0AuthenticationService(StrengthJournalContext context, UserService userService, ILogger<Auth0AuthenticationService> logger)
        {
            clientSecret = StrengthJournalConfiguration.Instance.Auth0_ClientSecret;
            clientId = StrengthJournalConfiguration.Instance.Auth0_ClientId;
            audience = StrengthJournalConfiguration.Instance.Auth0_Audience;
            client = new RestClient(StrengthJournalConfiguration.Instance.Auth0_BaseURL);
            connection = "Username-Password-Authentication";
            this.context = context;
            this.userService = userService;
            this.logger = logger;
        }

        public AuthenticationResponse Authenticate(string username, string password)
        {
            try
            {
                var request = new RestRequest("oauth/token", Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                var body = UrlEncode(new Dictionary<string, string>()
                {
                    { "grant_type", "password" },
                    { "scope", "openid" },
                    { "username", username },
                    { "password", password },
                    { "audience", audience },
                    { "client_id", clientId },
                    { "client_secret", clientSecret }
                });
                request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new AuthenticationResponse
                    {
                        Result = AuthenticationResponse.AuthResult.WrongPassword
                    };
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = ExtractTokenFromResponse(response.Content);
                    var userInfoRequest = new RestRequest("userinfo", Method.Get);
                    userInfoRequest.AddOrUpdateHeader("Authorization", $"Bearer {token}");
                    var userInfoResponse = client.Execute(userInfoRequest);
                    userInfoResponse.ThrowIfError();
                    dynamic profileData = JsonConvert.DeserializeObject(userInfoResponse.Content);
                    if (profileData.email_verified != "True")
                    {
                        return new AuthenticationResponse()
                        {
                            Result = AuthenticationResponse.AuthResult.EmailNotVerified
                        };
                    }
                    return new AuthenticationResponse()
                    {
                        Result = AuthenticationResponse.AuthResult.Success,
                        Token = token,
                        Profile = profileData
                    };
                }
                else
                {
                    logger.Log(LogLevel.Error, $"Auth0 authorization failure ${response.StatusCode} ${response.Content}");
                    return new AuthenticationResponse()
                    {
                        Result = AuthenticationResponse.AuthResult.ServiceFailure
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Authentication failure");
                return new AuthenticationResponse()
                {
                    Result = AuthenticationResponse.AuthResult.ServiceFailure
                };
            }
        }

        public CreateAccountResponse CreateAccount(string username, string password, bool consentCEM, string countryCode)
        {
            var request = new RestRequest("dbconnections/signup", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var body = UrlEncode(new Dictionary<string, string>()
            {
                { "email", username },
                { "password", password },
                { "audience", audience },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "connection", connection }
            });
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            try
            {
                dynamic responseData = JsonConvert.DeserializeObject(response.Content);
                if (response.IsSuccessStatusCode)
                {
                    var userId = $"auth0|{responseData._id}";
                    userService.RegisterUser(username, userId, consentCEM, countryCode);
                    return new CreateAccountResponse()
                    {
                        Result = CreateAccountResponse.CreateResult.Success
                    };
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new CreateAccountResponse()
                    {
                        Result = CreateAccountResponse.CreateResult.ValidationError,
                        ErrorMessage = responseData.message ?? responseData.description
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create account failure");
                return new CreateAccountResponse()
                {
                    Result = CreateAccountResponse.CreateResult.ServiceFailure,
                    ErrorMessage = ex.Message
                };
            }
            return new CreateAccountResponse()
            {
                Result = CreateAccountResponse.CreateResult.ServiceFailure,
                ErrorMessage = "Unknown Error"
            };
        }

        public bool ResetPassword(string username)
        {
            var request = new RestRequest("dbconnections/change_password", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var body = UrlEncode(new Dictionary<string, string>()
            {
                { "email", username },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "connection", connection }
            });
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response.IsSuccessStatusCode;
        }

        public bool ResendVerificationEmail(string username)
        {
            try
            {
                var token = GetManagementToken();
                var userId = GetUserIdByEmail(username, token);
                var request = new RestRequest("api/v2/jobs/verification-email", Method.Post);
                request.AddHeader("Authorization", $"Bearer {token}");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                var body = UrlEncode(new Dictionary<string, string>()
                {
                    { "user_id", $"auth0|{userId}" }
                });
                request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Resend verification failure");
                return false;
            }
        }

        public bool UpdateEmailAddress(string externalUserId, string newEmail)
        {
            try
            {
                var token = GetManagementToken();
                var request = new RestRequest($"api/v2/users/{externalUserId}", Method.Patch);
                request.AddHeader("Authorization", $"Bearer {token}");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                var body = UrlEncode(new Dictionary<string, string>()
                {
                    { "email", newEmail }
                });
                request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.IsSuccessStatusCode)
                {
                    userService.UpdateEmailAddress(externalUserId, newEmail);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update email failure");
                return false;
            }
        }

        private string ExtractTokenFromResponse(string response)
        {
            dynamic responseData = JsonConvert.DeserializeObject(response);
            return responseData.access_token;
        }

        private string UrlEncode(IDictionary<string, string> bodyData)
        {
            var keyVals = bodyData.Select(keyval => $"{keyval.Key}={keyval.Value}");
            return string.Join("&", keyVals);
        }

        private string GetManagementToken()
        {
            var request = new RestRequest("oauth/token", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            var body = UrlEncode(new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials" },
                { "audience", $"{StrengthJournalConfiguration.Instance.Auth0_BaseURL}api/v2/" },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            });
            request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return ExtractTokenFromResponse(response.Content);
        }

        private string GetUserIdByEmail(string email, string token)
        {
            var request = new RestRequest($"/api/v2/users?q=email:\"{email}\"&search_engine=v3", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute(request);
            dynamic responseData = JsonConvert.DeserializeObject(response.Content);
            return responseData[0].identities[0].user_id;
        }
    }
}
