namespace UrlShortener.Application.v1.Urls.Common.Models;

public sealed record ShortUrlOutput
{
    public required string Code { get; init; }

    public required Uri ShortenedUrl { get; init; }

    public required DateTimeOffset CreationDate { get; init; }

    public DateTimeOffset? ExpirationDate { get; init; }
}
