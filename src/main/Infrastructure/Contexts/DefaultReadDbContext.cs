using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.v1.Drivers.Common.Models;
using UrlShortener.Infrastructure.Mappings.ReadModels;

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

        modelBuilder.ApplyConfiguration(new DriverDtoMapping());

        modelBuilder.Entity<DriverDto>()
            .HasQueryFilter(field => field.DeletedAt == null);

        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}
