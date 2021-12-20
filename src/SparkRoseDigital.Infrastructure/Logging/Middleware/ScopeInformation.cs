using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SparkRoseDigital.Infrastructure.Logging.Middleware
{
    public class ScopeInformation : IScopeInformation
    {
        public Dictionary<string, string> Host { get; } = new Dictionary<string, string>
            {
                { "MachineName", Environment.MachineName }
            };

        public Dictionary<string, string> User(HttpContext context) =>
            new ()
            {
                {
                    "UserId",
                    context.User.Identity.IsAuthenticated
                        ? context.User.Claims.Single(c => c.Type == "sub").ToString()
                        : "Anonymous"
                },
                {
                    "Claims",
                    string.Join(';', context.User.Claims.Select(claim => claim.ToString()))
                }
            };
    }
}
