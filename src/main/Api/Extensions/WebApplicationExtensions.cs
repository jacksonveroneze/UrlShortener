using CorrelationId;
using Prometheus;
using Scalar.AspNetCore;
using Serilog;
using UrlShortener.Api.Endpoints.V1.Urls;
using ILogger = Serilog.ILogger;

namespace UrlShortener.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseCorrelationId();

        app.UseRequestLocalization();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseRouting();

        app.UseHttpMetrics();

        app.UseHealthChecks("/health");
        app.UseMetricServer();
        
        app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics-open");

        app.UseAuthentication();
        app.UseAuthorization();

        app.AddUrlsEndpoints();

        app.Lifetime.ApplicationStarted.Register(() =>
        {
            ILogger logger = app.Services.GetRequiredService<ILogger>();
            
            logger.Information("-> ApplicationStarted");
            
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStarted));
        });

        app.Lifetime.ApplicationStopping.Register(() =>
        {
            ILogger logger = app.Services.GetRequiredService<ILogger>();
            
            logger.Information("-> ApplicationStopping");
            
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopping));
        });

        app.Lifetime.ApplicationStopped.Register(() =>
        {
            ILogger logger = app.Services.GetRequiredService<ILogger>();
            
            logger.Information("-> ApplicationStopped");
            
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopped));
        });

        return app;
    }
}
