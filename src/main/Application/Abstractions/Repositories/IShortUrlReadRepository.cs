using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Application.Abstractions.Repositories;

public interface IShortUrlReadRepository
{
    public Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken);
    
    public Task<ShortUrl?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken);
}
