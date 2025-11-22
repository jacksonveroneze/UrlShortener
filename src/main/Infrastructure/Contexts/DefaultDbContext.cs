using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Aggregates;
using UrlShortener.Infrastructure.Extensions;
using UrlShortener.Infrastructure.Mappings.Domain;

namespace UrlShortener.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class DefaultDbContext(
    DbContextOptions<DefaultDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new DriverMapping());

        modelBuilder.ApplySoftDeleteQueryFilter<Driver>();

        modelBuilder.IgnoreClass();

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
