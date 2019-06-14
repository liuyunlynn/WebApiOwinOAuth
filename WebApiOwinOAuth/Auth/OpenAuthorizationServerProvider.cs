using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using WebApiOwinOAuth.Services;

namespace WebApiOwinOAuth.Auth
{
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public OpenAuthorizationServerProvider(UserService service)
        {
            _service = service;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                string clientId;
                string clientSecret;

                if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                {
                    context.TryGetFormCredentials(out clientId, out clientSecret);
                }
                context.TryGetBasicCredentials(out clientId, out clientSecret);



                var result = _service.Login(clientId, clientSecret);

                if (string.IsNullOrEmpty(result))
                {
                    context.SetError("invalid_client", "User Name or Password is not valid");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result));

                context.Validated();
                await base.ValidateClientAuthentication(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantClientCredentials(context);
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);
        }
        private readonly UserService _service;
    }
}
