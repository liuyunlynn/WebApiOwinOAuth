
namespace WebApiOwinOAuth.Services
{
    public class UserService
    {
        public string Login(string userName, string password)
        {
            var userId = string.Empty;
            if (userName == "123" && password == "456")
            {
                userId = "user123";
            }
            return userId;
        }
    }
}