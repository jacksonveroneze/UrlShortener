using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultReadDbContext(
    DbContextOptions<DefaultReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
