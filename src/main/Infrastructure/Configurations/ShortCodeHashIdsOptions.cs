namespace UrlShortener.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record ShortCodeHashIdsOptions
{
    public const string SectionName = "ShortCodeHashIds";

    public string? Salt { get; init; }

    public int MinLength { get; init; }

    public string? Alphabet { get; init; }
}
