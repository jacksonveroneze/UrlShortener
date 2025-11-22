namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    [Required]
    public string? Environment { get; init; }

    public AppInfoConfiguration? Application { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public AuthConfiguration? Auth { get; init; }

    public DatabaseConfiguration? Database { get; init; }
    
    public CacheConfiguration? Cache { get; init; }

    public string AppName =>
        Application!.Name!;

    public Version AppVersion =>
        Application!.Version!;

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}
