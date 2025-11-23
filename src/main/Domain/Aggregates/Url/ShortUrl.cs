using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Aggregates.Url;

public class ShortUrl : AggregateRoot
{
    public Guid Id { get; private set; }

    public Uri? OriginalUrl { get; private set; }

    public DateTime ExpiresdAt { get; private set; }


    #region ctor

    protected ShortUrl()
    {
    }

    private ShortUrl(Guid id,
        Uri originalUrl,
        DateTime expiresdAt)
    {
        ArgumentNullException.ThrowIfNull(originalUrl);

        Id = id;
        OriginalUrl = originalUrl;
        ExpiresdAt = expiresdAt;
    }

    #endregion

    #region Factory

    public static Result<ShortUrl> Create(
        Guid id,
        Uri originalUrl,
        DateTime expiresdAt)
    {
        ShortUrl entity = new(id, originalUrl, expiresdAt);

        return Result<ShortUrl>.WithSuccess(entity);
    }

    #endregion
}
