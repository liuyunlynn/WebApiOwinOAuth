using System.Net.Http;

namespace RestfulWebService.Infrastructure.Extensions
{
    public interface IHttpClientManager
    {
        HttpClient GetOrAdd(string baseUrl);
        HttpClient GetOrAdd(string baseUrl,string token);
        void Clear();
    }
}
