using JacksonVeroneze.NET.Pagination.Offset;
using UrlShortener.Application.v1.Drivers.Common.Filters;
using UrlShortener.Application.v1.Drivers.Common.Models;

namespace UrlShortener.Application.Common.Interfaces.Repositories;

public interface IDriverReadRepository
{
    public Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken);
}
