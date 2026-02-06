using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("providers");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.ContactType).HasColumnName("contact_type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.Contact).HasColumnName("contact").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Country).HasColumnName("country").HasMaxLength(100).IsRequired();
        builder.Property(e => e.City).HasColumnName("city").HasMaxLength(100).IsRequired();
        builder.Property(e => e.LinkFromWhereFoundAbroad).HasColumnName("link_from_where_found_abroad").HasMaxLength(500).IsRequired();
    }
}

public class ProductProviderConfiguration : IEntityTypeConfiguration<ProductProvider>
{
    public void Configure(EntityTypeBuilder<ProductProvider> builder)
    {
        builder.ToTable("product_providers");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ProductId).HasColumnName("product_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.ProviderId).HasColumnName("provider_id").HasMaxLength(36).IsRequired();

        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductProviders)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Provider)
            .WithMany(p => p.ProductProviders)
            .HasForeignKey(e => e.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
