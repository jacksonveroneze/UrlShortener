using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Aggregates;

namespace UrlShortener.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class DriverMapping : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.Status);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(conf => conf.Name, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(conf => conf.Email, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasMaxLength(100)
                .HasColumnName("email")
                .IsRequired();
        });

        builder.ComplexProperty(conf => conf.Cpf, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasMaxLength(11)
                .HasColumnName("cpf")
                .IsRequired();
        });


        builder.Property(c => c.Status)
            .IsRequired();

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
