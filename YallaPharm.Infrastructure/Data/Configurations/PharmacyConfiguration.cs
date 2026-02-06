using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
{
    public void Configure(EntityTypeBuilder<Pharmacy> builder)
    {
        builder.ToTable("pharmacies");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100);
        builder.Property(e => e.Address).HasColumnName("address").HasMaxLength(500);
        builder.Property(e => e.Landmark).HasColumnName("landmark").HasMaxLength(500);
        builder.Property(e => e.Contact).HasColumnName("contact").HasMaxLength(100);
        builder.Property(e => e.GeolocationLink).HasColumnName("geolocation_link").HasMaxLength(500);
        builder.Property(e => e.Country).HasColumnName("country").HasMaxLength(100);
        builder.Property(e => e.Markup).HasColumnName("markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.MarkupType).HasColumnName("markup_type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.IsRequired).HasColumnName("is_required").HasDefaultValue(false);
        builder.Property(e => e.IsAbroad).HasColumnName("is_abroad").HasDefaultValue(false);
        builder.Property(e => e.PayoutMethod).HasColumnName("payout_method").HasMaxLength(50).HasConversion<string>();
    }
}
