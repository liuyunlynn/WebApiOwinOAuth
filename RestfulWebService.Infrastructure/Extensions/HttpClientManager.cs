using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestfulWebService.Infrastructure.Extensions
{
    public class HttpClientManager : IHttpClientManager
    {
        public HttpClient GetOrAdd(string baseUrl)
        {
            lock (locker)
            {
                if (httpClients.ContainsKey(baseUrl))
                {
                    return httpClients[baseUrl];
                }

                var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })
                {
                    BaseAddress = new Uri(baseUrl),
                };
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
        }

        public HttpClient GetOrAdd(string baseUrl,string token)
        {
            lock (locker)
            {
                if (httpClients.ContainsKey(baseUrl))
                {
                    return httpClients[baseUrl];
                }

                var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })
                {
                    BaseAddress = new Uri(baseUrl),
                };
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return httpClient;
            }
        }


        public void Clear()
        {
            httpClients.Clear();
        }

        #region fields

        private IDictionary<string, HttpClient> httpClients = new Dictionary<string, HttpClient>();
        private static object locker = new object();

        #endregion

    }
}
