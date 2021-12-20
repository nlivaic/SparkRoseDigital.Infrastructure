using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SparkRoseDigital.Infrastructure.Logging.Middleware
{
    public class UserLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserLoggingMiddleware> _logger;
        private readonly IScopeInformation _scopeInformation;
        private IDisposable _userScope;

        public UserLoggingMiddleware(
            RequestDelegate next,
            ILogger<UserLoggingMiddleware> logger,
            IScopeInformation scopeInformation)
        {
            _next = next;
            _logger = logger;
            _scopeInformation = scopeInformation;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _userScope = _logger.BeginScope(_scopeInformation.User(context));
                await _next(context);
            }
            finally
            {
                _userScope.Dispose();
            }
        }
    }
}
