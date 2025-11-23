using JacksonVeroneze.NET.EntityFramework.Interfaces;
using UrlShortener.Domain.Repositories;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.Repositories.ShortUrl;

[ExcludeFromCodeCoverage]
public class ShortUrlRepository(
    IEfCoreRepository<Domain.Aggregates.Url.ShortUrl, DefaultDbContext> service)
    : IShortUrlRepository
{
    public async Task CreateAsync(
        Domain.Aggregates.Url.ShortUrl entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }
}
