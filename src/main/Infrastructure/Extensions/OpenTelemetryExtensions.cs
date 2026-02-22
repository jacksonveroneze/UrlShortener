using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        bool isEnabled = appConfiguration
            .DistributedTracing?.IsEnabled ?? false;

        if (!isEnabled)
        {
            return services;
        }

        services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
        {
            options.Filter = ctx =>
                (!ctx.Request.Path.Value?.StartsWith("/metrics",
                    StringComparison.OrdinalIgnoreCase) ?? false) &&
                ctx.Request.Path != "/health";
        });

        services.AddOpenTelemetry()
            .ConfigureResource(ConfigureResource)
            .AddMetrics()
            .AddTracing(appConfiguration);

        return services;

        void ConfigureResource(ResourceBuilder r)
        {
            r.AddService(
                appConfiguration.AppName,
                serviceVersion: appConfiguration.AppVersion.ToString(),
                serviceInstanceId: Environment.MachineName);
        }
    }

    extension(IOpenTelemetryBuilder builder)
    {
        private IOpenTelemetryBuilder AddMetrics()
        {
            builder.WithMetrics(opts => opts
                .AddFusionCacheInstrumentation()
                .AddProcessInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMeter("Polly")
                .AddPrometheusExporter()
                .AddRuntimeInstrumentation());

            return builder;
        }

        private IOpenTelemetryBuilder AddTracing(AppConfiguration appConfiguration)
        {
            DistributedTracingToolConfiguration configuration =
                appConfiguration.DistributedTracing!.Jaeger!;

            builder.WithTracing(conf =>
            {
                conf.AddAspNetCoreInstrumentation(options => { options.RecordException = true; })
                    .AddEntityFrameworkCoreInstrumentation(options =>
                    {
                        options.SetDbStatementForText = true;
                        options.SetDbStatementForStoredProcedure = true;
                    })
                    .AddHttpClientInstrumentation()
                    .AddRedisInstrumentation()
                    .AddSource("TaxiService");

                conf.AddOtlpExporter(config => config.Endpoint =
                    new Uri($"http://{configuration.Host}:{configuration.Port}"));
            });

            return builder;
        }
    }
}
