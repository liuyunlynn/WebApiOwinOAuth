using System.Web.Http;
using OwinOAuth.Dtos;

namespace WebApiOwinOAuth.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public UserDto GetUserProfileById(string userId)
        {
            var user=new UserDto()
            {
                Name = "Jack",
                UserId = userId
            };
            return user;
        }

    }
}