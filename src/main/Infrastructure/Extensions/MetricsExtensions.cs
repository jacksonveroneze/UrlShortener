using Prometheus.DotNetRuntime;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MetricsExtensions
{
    public static IDisposable? AddMetrics()
    {
        return DotNetRuntimeStatsBuilder
            .Customize()
            .WithContentionStats(CaptureLevel.Informational)
            .WithJitStats(CaptureLevel.Informational)
            .WithThreadPoolStats(CaptureLevel.Informational)
            .WithGcStats(CaptureLevel.Informational)
            .WithExceptionStats()
            .StartCollecting();
    }
}
