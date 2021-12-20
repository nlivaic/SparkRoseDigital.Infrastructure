using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SparkRoseDigital.Infrastructure.Logging.Middleware
{
    /// <summary>
    /// Add additional entries to Host, e.g. EntryAssembly.
    /// </summary>
    public interface IScopeInformation
    {
        Dictionary<string, string> Host { get; }

        Dictionary<string, string> User(HttpContext context);
    }
}
