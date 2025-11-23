namespace UrlShortener.Application.v1.Urls.Create;

public sealed record ShortUrlOutput
{
    public required Uri ShortenedUrl { get; init; }

    public required DateTimeOffset CreationDate { get; init; }

    public required DateTimeOffset ExpirationDate { get; init; }
}
