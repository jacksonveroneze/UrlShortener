namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record CacheConfiguration
{
    public CacheType Type { get; init; }

    public string? Endpoint { get; init; }
}

public enum CacheType
{
    Memory,
    Distributed
}
