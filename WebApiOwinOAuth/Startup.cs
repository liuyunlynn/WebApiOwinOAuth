using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApiOwinOAuth;
using WebApiOwinOAuth.Auth;
using WebApiOwinOAuth.Services;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApiOwinOAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var userService = new UserService();

            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AuthorizeEndpointPath = new PathString("/api/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = new OpenAuthorizationServerProvider(userService),
                RefreshTokenProvider = new OpenRefreshTokenProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}