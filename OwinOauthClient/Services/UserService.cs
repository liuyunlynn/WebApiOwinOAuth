using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using OwinOAuth.Dtos;
using OwinOauthClient.Interfaces;
using RestfulWebService.Infrastructure.Common;
using RestfulWebService.Infrastructure.Extensions;

namespace OwinOauthClient.Services
{
    public class UserService : IUserService
    {
        public UserService(string baseUrl)
        {
            _httpClientManager = new HttpClientManager();
            _baseUrl = baseUrl;
        }
        public async Task<HttpWebApiResponse<UserDto>> GetUserProfileById(string userId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("userId", userId);
            var client = _httpClientManager.GetOrAdd(_baseUrl, AccessToken);
            var response = await client.ExtensionGetAsync<UserDto>("User/GetUserProfileById",
                parameters);
            return response;
        }

        public async Task<HttpWebApiResponse<string>> ValidateUser(string userName, string password)
        {
            try
            {
                var clientId = userName;
                var clientSecret = password;
                var parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                parameters.Add(new KeyValuePair<string, string>("role", "Beneficiary"));
                var client = _httpClientManager.GetOrAdd(_baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));
                var response = await client.ExtensionPostAsync<string>("token", parameters);
                return response;
            }
            catch (Exception e)
            {
                var result = new HttpWebApiResponse<string> { ErrorMessage = e.Message };
                return result;
            }
        }

        #region fields
        private IHttpClientManager _httpClientManager;
        private string _baseUrl;
        public string AccessToken { get; set; }
        #endregion
    };
  
}
