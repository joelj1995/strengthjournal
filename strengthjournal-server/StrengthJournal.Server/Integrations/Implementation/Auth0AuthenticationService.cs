﻿using Newtonsoft.Json;
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
            clientSecret = StrengthJournalConfiguration.Instance.Auth0_ClientSecret;
            clientId = StrengthJournalConfiguration.Instance.Auth0_ClientId;
            audience = StrengthJournalConfiguration.Instance.Auth0_Audience;
            client = new RestClient(StrengthJournalConfiguration.Instance.Auth0_BaseURL);
            connection = "Username-Password-Authentication";
        }

        protected string ExtractTokenFromResponse(string response)
        {
            dynamic responseData = JsonConvert.DeserializeObject(response);
            return responseData.access_token;
        }

        private string UrlEncode(IDictionary<string,string> bodyData)
        {
            var keyVals = bodyData.Select(keyval => $"{keyval.Key}={keyval.Value}");
            return string.Join("&", keyVals);
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
