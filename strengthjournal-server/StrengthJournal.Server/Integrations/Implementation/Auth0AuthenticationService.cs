using Newtonsoft.Json;
using RestSharp;
using StrengthJournal.Server.Integrations.Models;
using System.Net;

namespace StrengthJournal.Server.Integrations.Implementation
{
    public class Auth0AuthenticationService : IAuthenticationService
    {
        private readonly RestClient client;

        private readonly string clientSecret;
        private readonly string clientId;
        private readonly string audience;
        private readonly string connection;

        public Auth0AuthenticationService()
        {
            clientSecret = "XtCYN0xD2JrDyZN6hFj37qm8aVkyGhJmPipPbS3xcZfaoHmfB3Yb4txgGUsZJq__"; // TODO: inject this
            clientId = "KYRJbp51UUhR91b1B1oGWh3zgbpAmNau";
            audience = "https://localhost:7080/api";
            client = new RestClient("https://dev-bs65rtlog25jigd0.us.auth0.com/");
            connection = "Username-Password-Authentication";
        }

        protected string ExtractTokenFromResponse(string response)
        {
            dynamic responseData = JsonConvert.DeserializeObject(response);
            return responseData.access_token;
        }

        public AuthenticationResponse Authenticate(string username, string password)
        {
            try
            {
                var request = new RestRequest("oauth/token", Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", $"grant_type=password&username={username}&password={password}&audience={audience}&client_id={clientId}&client_secret={clientSecret}", ParameterType.RequestBody);
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
                    return new AuthenticationResponse()
                    {
                        Result = AuthenticationResponse.AuthResult.Success,
                        Token = ExtractTokenFromResponse(response.Content)
                    };
                }
                else
                {
                    return new AuthenticationResponse()
                    {
                        Result = AuthenticationResponse.AuthResult.ServiceFailure
                    };
                }
            }
            catch
            {
                return new AuthenticationResponse()
                {
                    Result = AuthenticationResponse.AuthResult.ServiceFailure
                };
            }
        }

        public CreateAccountResponse CreateAccount(string username, string password)
        {
            var request = new RestRequest("dbconnections/signup", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"email={username}&password={password}&audience={audience}&client_id={clientId}&client_secret={clientSecret}&connection={connection}", ParameterType.RequestBody);
            var response = client.Execute(request);
            try 
            {
                if (response.IsSuccessStatusCode)
                {
                    return new CreateAccountResponse()
                    {
                        Result = CreateAccountResponse.CreateResult.Success
                    };
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    dynamic responseData = JsonConvert.DeserializeObject(response.Content);
                    return new CreateAccountResponse()
                    {
                        Result = CreateAccountResponse.CreateResult.ValidationError,
                        ErrorMessage = responseData.message
                    };
                }
            }
            catch (Exception ex)
            {
                // TODO: Add some logging
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
    }
}
