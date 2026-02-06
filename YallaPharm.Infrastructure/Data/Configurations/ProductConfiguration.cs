using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Dosage).HasColumnName("dosage").HasMaxLength(5000).IsRequired();
        builder.Property(e => e.CountOnPackage).HasColumnName("count_on_package");
        builder.Property(e => e.AgeFrom).HasColumnName("age_from");
        builder.Property(e => e.AgeTo).HasColumnName("age_to");
        builder.Property(e => e.PriceWithMarkup).HasColumnName("price_with_markup").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.PriceWithoutMarkup).HasColumnName("price_without_markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.Manufacturer).HasColumnName("manufacturer").HasMaxLength(50).IsRequired();
        builder.Property(e => e.PathImage).HasColumnName("path_image").HasMaxLength(500).IsRequired();
        builder.Property(e => e.Dificit).HasColumnName("dificit").HasDefaultValue(false);
        builder.Property(e => e.ReleaseForm).HasColumnName("release_form").HasMaxLength(50).IsRequired();
        builder.Property(e => e.PackagingUnit).HasColumnName("packaging_unit").HasMaxLength(50).IsRequired();
        builder.Property(e => e.TypeOfPackaging).HasColumnName("type_of_packaging").HasMaxLength(50).IsRequired();
        builder.Property(e => e.LinkProduct).HasColumnName("link_product").HasMaxLength(5000).IsRequired();
        builder.Property(e => e.Country).HasColumnName("country").HasMaxLength(500).IsRequired();
        builder.Property(e => e.AgeType).HasColumnName("age_type").HasMaxLength(20).HasConversion<string>();
        builder.Property(e => e.IsRequired).HasColumnName("is_required").HasDefaultValue(false);
        builder.Property(e => e.State).HasColumnName("state").HasMaxLength(20).HasConversion<string>();

        builder.HasIndex(e => e.Name).HasDatabaseName("idx_products_name");
    }
}
