using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Application.v1.Drivers.Common.Models;

namespace UrlShortener.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class DriverDtoMapping : IEntityTypeConfiguration<DriverDto>
{
    public void Configure(EntityTypeBuilder<DriverDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver");

        // Keys
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Id);
        builder.Property(c => c.FullName).HasColumnName("name");
        builder.Property(c => c.Email);
        builder.Property(c => c.Status);
        builder.Property(c => c.Document).HasColumnName("cpf");
        builder.Property(c => c.DeletedAt);
    }
}
