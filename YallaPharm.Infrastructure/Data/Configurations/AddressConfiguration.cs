using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ClientId).HasColumnName("client_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.Street).HasColumnName("street").HasMaxLength(500).IsRequired();
        builder.Property(e => e.Landmark).HasColumnName("landmark").HasMaxLength(500).IsRequired();
        builder.Property(e => e.City).HasColumnName("city").HasMaxLength(100).IsRequired();
        builder.Property(e => e.GeolocationOfClientAddress).HasColumnName("geolocation_of_client_address").HasMaxLength(500);

        builder.HasOne(e => e.Client)
            .WithMany(c => c.Addresses)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
