using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SparkRoseDigital.Infrastructure.Logging
{
    public static class SerilogConfiguration
    {
        public static Action<HostBuilderContext, IServiceProvider, LoggerConfiguration> ConfigureLogger =
            (HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            loggerConfiguration
                .WriteTo.ApplicationInsights(
                    services.GetRequiredService<TelemetryConfiguration>(),
            TelemetryConverter.Traces);
    }
}
