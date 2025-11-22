using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using UrlShortener.Domain;
using UrlShortener.Domain.Repositories;
using UrlShortener.Infrastructure.Contexts;
using UrlShortener.Infrastructure.Extensions;

namespace UrlShortener.Infrastructure.Repositories.Drivers;

[ExcludeFromCodeCoverage]
public class DriverRepository(
    ILogger<DriverRepository> logger,
    IEfCoreRepository<Domain.Aggregates.Driver, DefaultDbContext> service)
    : IDriverRepository
{
    public async Task<Domain.Aggregates.Driver?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Domain.Aggregates.Driver? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(DriverRepository),
            nameof(GetByIdAsync),
            id, result is not null);

        return result;
    }

    public async Task CreateAsync(Domain.Aggregates.Driver entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(Domain.Aggregates.Driver entity,
        CancellationToken cancellationToken)
    {
        entity.MarkAsDeleted();

        service.SoftDelete(entity);

        return service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Aggregates.Driver entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
