using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Pagination.Offset;
using JacksonVeroneze.NET.Pagination.Offset.Extensions;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Application.v1.Drivers.Common.Filters;
using UrlShortener.Application.v1.Drivers.Common.Models;
using UrlShortener.Domain;
using UrlShortener.Infrastructure.Builders.Filters;
using UrlShortener.Infrastructure.Contexts;
using UrlShortener.Infrastructure.Extensions;

namespace UrlShortener.Infrastructure.Repositories.Drivers;

[ExcludeFromCodeCoverage]
public class DriverReadRepository(
    ILogger<DriverReadRepository> logger,
    IEfCoreRepository<DriverDto, DefaultReadDbContext> service)
    : IDriverReadRepository
{
    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(
            conf => conf.Email!.Equals(email.ToUpperInvariant()),
            cancellationToken);

        logger.LogExistsByValue(
            ModuleConstants.ContextName,
            nameof(DriverReadRepository),
            nameof(ExistsByEmailAsync),
            email, exists);

        return exists;
    }

    public async Task<DriverDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        Expression<Func<DriverDto, bool>> spec
            = entity => entity.Id == id;

        DriverDto? result = await service
            .GetByIdAsync(spec, cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(DriverReadRepository),
            nameof(GetByIdAsync),
            id, result != null);

        return result;
    }

    public async Task<Page<DriverDto>> GetPagedAsync(
        DriverPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Expression<Func<DriverDto, bool>> expression =
            DriverPagedFilterBuilder.Create(filter).Build();

        long count = await service.CountAsync(
            expression, cancellationToken);

        ICollection<DriverDto> result =
            await service.GetAllAsync(
                expression,
                order => order.Id,
                filter.Pagination!.PageSize,
                cancellationToken: cancellationToken);

        return result.ToPage(filter.Pagination, (int)count);
    }
}
