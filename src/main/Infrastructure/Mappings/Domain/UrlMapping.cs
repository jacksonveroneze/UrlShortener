using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class UrlMapping : IEntityTypeConfiguration<ShortUrl>
{
    public void Configure(EntityTypeBuilder<ShortUrl> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("url");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        //builder.HasIndex(c => c.Status);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();
        
        builder.Property(c => c.OriginalUrl)
            .HasColumnName("original_url")
            .HasMaxLength(1_000)
            .IsRequired();
        
        builder.Property(c => c.ExpiresdAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
