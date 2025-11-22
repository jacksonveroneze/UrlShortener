using UrlShortener.Domain.Aggregates;

namespace UrlShortener.Domain.Repositories;

public interface IDriverRepository
{
    public Task<Driver?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Driver entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Driver entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Driver entity,
        CancellationToken cancellationToken);
}
