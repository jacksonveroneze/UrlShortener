namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DistributedTracingConfiguration
{
    public bool IsEnabled { get; init; }

    public DistributedTracingToolConfiguration? Jaeger { get; init; }
}

[ExcludeFromCodeCoverage]
public sealed record DistributedTracingToolConfiguration
{
    public string? Host { get; init; }

    public int? Port { get; init; }
}
