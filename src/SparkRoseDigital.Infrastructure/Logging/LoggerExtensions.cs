﻿using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using SparkRoseDigital.Infrastructure.Logging.Middleware;

namespace SparkRoseDigital.Infrastructure.Logging
{

    public static class LoggerExtensions
    {
        public static void ConfigureSerilogLogger(string environmentVariable)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(environmentVariable)}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Adding Entrypoint here means it is added to every log,
            // regardless if it comes from Hosting or the application itself.
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Entrypoint", Assembly.GetEntryAssembly().GetName().Name)
                .Enrich.WithSpan()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(configuration)
                .CreateBootstrapLogger();
        }

        /// <summary>
        /// Use for debugging! Adds a listener so every new Activity is logged.
        /// Makes tracing easier due to some components (e.g. HttpClient, MassTransit)
        /// create their own Activity and the attached listener allows end-to-end tracing.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <returns>Host with activity logging configured.</returns>
        public static IHost AddActivityLogging(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Activity>>();
                ActivitySource.AddActivityListener(new ActivityListener
                {
                    ShouldListenTo = _ => true,
                    ActivityStarted = activity =>
                    {
                        logger.LogTrace("Activity started: {operationName}.", activity.OperationName);
                    },
                    ActivityStopped = activity =>
                    {
                        logger.LogTrace("Activity stopped: {operationName}.", activity.OperationName);
                    }
                });
            }
            return host;
        }

        public static void AddLoggingScopes(this IServiceCollection services) => services.AddSingleton<IScopeInformation, ScopeInformation>();

        public static IApplicationBuilder UseHostLoggingMiddleware(
            this IApplicationBuilder builder) => builder.UseMiddleware<HostLoggingMiddleware>();

        public static IApplicationBuilder UseUserLoggingMiddleware(
            this IApplicationBuilder builder) => builder.UseMiddleware<UserLoggingMiddleware>();
    }
}
