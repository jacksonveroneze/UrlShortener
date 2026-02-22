using CorrelationId;
using Prometheus;
using Scalar.AspNetCore;
using Serilog;
using UrlShortener.Api.Endpoints.V1.Urls;

namespace UrlShortener.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // Correlation ID as early as possible so all subsequent components can use it
        app.UseCorrelationId();

        // Localization early so culture is set before routing, validation, etc.
        app.UseRequestLocalization();

        // Error handling and status code pages
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        // Routing before metrics and auth to enrich labels with route data
        app.UseRouting();

        // HTTP request metrics around most of the pipeline, but before auth/endpoints
        app.UseHttpMetrics();

        // Infra endpoints should remain unauthenticated
        app.UseHealthChecks("/health");
        app.UseMetricServer();
        app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics-open");

        // Security
        app.UseAuthentication();
        app.UseAuthorization();

        app.AddUrlsEndpoints();

        app.Lifetime.ApplicationStarted.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStarted)));

        app.Lifetime.ApplicationStopping.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopping)));

        app.Lifetime.ApplicationStopped.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopped)));

        return app;
    }
}
