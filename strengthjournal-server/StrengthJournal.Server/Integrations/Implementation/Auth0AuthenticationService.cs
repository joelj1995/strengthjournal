using Newtonsoft.Json;
using RestSharp;
using StrengthJournal.Server.Integrations.Models;
using System.Net;

namespace StrengthJournal.Server.Integrations.Implementation
{
    public class Auth0AuthenticationService : IAuthenticationService
    {
        protected string ExtractTokenFromResponse(string response)
        {
            dynamic responseData = JsonConvert.DeserializeObject(response);
            return responseData.access_token;
        }

        public AuthenticationResponse Authenticate(string username, string password)
        {
            try
            {
                var clientSecret = "XtCYN0xD2JrDyZN6hFj37qm8aVkyGhJmPipPbS3xcZfaoHmfB3Yb4txgGUsZJq__"; // TODO: inject this
                var clientId = "KYRJbp51UUhR91b1B1oGWh3zgbpAmNau";
                var audience = "https://localhost:7080/api";
                var client = new RestClient("https://dev-bs65rtlog25jigd0.us.auth0.com/");
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
    }
}
