using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Domain.Repositories;

public interface IShortUrlRepository
{
    public Task CreateAsync(
        ShortUrl entity,
        CancellationToken cancellationToken);
}
