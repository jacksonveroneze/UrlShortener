using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Application.Abstractions.Repositories;

public interface IShortUrlReadRepository
{
    public Task<ShortUrl?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken);
}
