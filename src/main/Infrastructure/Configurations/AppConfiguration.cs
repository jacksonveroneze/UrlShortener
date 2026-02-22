namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public AppInfoConfiguration? Application { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public AuthConfiguration? Auth { get; init; }

    public DatabaseConfiguration? Database { get; init; }
    
    public CacheConfiguration? Cache { get; init; }

    public string AppName =>
        Application!.Name!;

    public Version AppVersion =>
        Application!.Version!;
}
