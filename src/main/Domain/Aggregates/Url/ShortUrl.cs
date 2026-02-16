using System.Text.Json.Serialization;
using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Aggregates.Url;

public class ShortUrl : AggregateRoot
{
    public string Code { get; init; } = null!;

    public Uri OriginalUrl { get; init; } = null!;

    public DateTimeOffset? ExpiresAt { get; init; }

    #region ctor

    [JsonConstructor]
    protected ShortUrl()
    {
    }

    private ShortUrl(
        ShortCode? code,
        Uri originalUrl,
        DateTimeOffset? expiresAt)
    {
        ArgumentNullException.ThrowIfNull(originalUrl);
        ArgumentNullException.ThrowIfNull(code);

        Code = code.Value;
        OriginalUrl = originalUrl;
        ExpiresAt = expiresAt;
    }

    #endregion

    public bool IsExpired(DateTimeOffset nowUtc)
    {
        return ExpiresAt.HasValue && ExpiresAt.Value <= nowUtc;
    }

    #region Factory

    internal static Result<ShortUrl> Create(
        ShortCode? code,
        Uri originalUrl,
        DateTimeOffset? expiresdAt)
    {
        ShortUrl entity = new(code, originalUrl, expiresdAt);

        return Result<ShortUrl>.WithSuccess(entity);
    }

    #endregion
}
