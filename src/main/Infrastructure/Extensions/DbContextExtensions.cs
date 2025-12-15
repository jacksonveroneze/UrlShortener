using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.DomainObjects.Messaging;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DbContextExtensions
{
    extension(ModelBuilder modelBuilder)
    {
        public void ApplySoftDeleteQueryFilter<TEntity>() where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(field => field.DeletedAt == null);
        }

        public void IgnoreClass()
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.Ignore<ShortCode>();
        }
    }
}
