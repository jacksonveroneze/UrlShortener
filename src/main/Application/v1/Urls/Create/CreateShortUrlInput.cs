using UrlShortener.Application.Abstractions.UseCases;

namespace UrlShortener.Application.v1.Urls.Create;

public sealed record CreateShortUrlInput
    : IBaseRequest
{
    public Uri? OriginalUrl { get; init; }

    public string? CustomAlias { get; init; }

    public DateTimeOffset? ExpirationDate { get; init; }
}
