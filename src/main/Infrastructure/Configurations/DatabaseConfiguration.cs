namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }

    public string? WriteConnectionString { get; init; }
}
