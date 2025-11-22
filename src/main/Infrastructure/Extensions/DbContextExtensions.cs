using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DbContextExtensions
{
    public static void ApplySoftDeleteQueryFilter<TEntity>(
        this ModelBuilder modelBuilder) where TEntity : Entity
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(field => field.DeletedAt == null);
    }

    public static void IgnoreClass(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<DomainEvent>();
    }
}
