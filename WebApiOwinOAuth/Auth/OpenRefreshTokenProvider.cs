using System;
using System.Collections.Concurrent;
using Microsoft.Owin.Security.Infrastructure;

namespace WebApiOwinOAuth.Auth
{
    public class OpenRefreshTokenProvider : AuthenticationTokenProvider
    {
        private static readonly ConcurrentDictionary<string, string> RefreshTokens = new ConcurrentDictionary<string, string>();

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(60);

            context.SetToken(Guid.NewGuid().ToString("n"));
            RefreshTokens[context.Token] = context.SerializeTicket();
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            if (RefreshTokens.TryRemove(context.Token, out var value))
            {
                context.DeserializeTicket(value);
            }
        }
    }
}