using System.Threading.Tasks;
using OwinOAuth.Dtos;
using RestfulWebService.Infrastructure.Common;

namespace OwinOauthClient.Interfaces
{
    public interface IUserService
    {
        string AccessToken { get; set; }
        Task<HttpWebApiResponse<string>> ValidateUser(string userName, string password);

        Task<HttpWebApiResponse<UserDto>> GetUserProfileById(string userId);
    }
}
