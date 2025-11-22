using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.UnitOfWork;

public class EfUnitOfWork(
    DefaultDbContext context) : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        int totalChanges = await context
            .SaveChangesAsync(cancellationToken);

        bool success = totalChanges > 0;

        return success;
    }
}
