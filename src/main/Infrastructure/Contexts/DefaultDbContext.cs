using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Aggregates.Url;
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

        modelBuilder.HasDefaultSchema(Constants.SchemaName);

        modelBuilder.ApplyConfiguration(new ShortUrlMapping());

        modelBuilder.ApplySoftDeleteQueryFilter<ShortUrl>();

        modelBuilder.IgnoreClass();
    }
}
