using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.Repositories.ShortUrl;

[ExcludeFromCodeCoverage]
public class ShortUrlReadRepository(
    IEfCoreRepository<Domain.Aggregates.Url.ShortUrl, DefaultReadDbContext> service)
    : IShortUrlReadRepository
{
    public Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        return service.DbSet.AnyAsync(
            conf => conf.Code == code,
            cancellationToken: cancellationToken);
    }

    public Task<Domain.Aggregates.Url.ShortUrl?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        return service.DbSet.FirstOrDefaultAsync(
            conf => conf.Code == code,
            cancellationToken: cancellationToken);
    }
}
