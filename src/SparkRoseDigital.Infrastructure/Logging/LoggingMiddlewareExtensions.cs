using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SparkRoseDigital.Infrastructure.Logging.Middleware;

namespace SparkRoseDigital.Infrastructure.Logging
{
    public static class LoggingMiddlewareExtensions
    {
        public static void AddLoggingScopes(this IServiceCollection services)
        {
            services.AddSingleton<IScopeInformation, ScopeInformation>();
        }

        public static IApplicationBuilder UseHostLoggingMiddleware(
            this IApplicationBuilder builder) => builder.UseMiddleware<HostLoggingMiddleware>();

        public static IApplicationBuilder UseUserLoggingMiddleware(
            this IApplicationBuilder builder) => builder.UseMiddleware<UserLoggingMiddleware>();
    }
}
