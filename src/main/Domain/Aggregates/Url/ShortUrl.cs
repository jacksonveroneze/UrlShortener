using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Aggregates.Url;

public class ShortUrl : AggregateRoot
{
    public Guid Id { get; private set; }

    public ShortCode Code { get; private set; } = null!;

    public Uri? OriginalUrl { get; private set; }

    public DateTimeOffset? ExpiresAt { get; private set; }


    #region ctor

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

        Id = Guid.CreateVersion7();
        Code = code;
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
