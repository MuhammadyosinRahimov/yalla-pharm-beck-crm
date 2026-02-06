using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ReleaseFormConfiguration : IEntityTypeConfiguration<ReleaseForm>
{
    public void Configure(EntityTypeBuilder<ReleaseForm> builder)
    {
        builder.ToTable("release_forms");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
    }
}

public class PackagingTypeConfiguration : IEntityTypeConfiguration<PackagingType>
{
    public void Configure(EntityTypeBuilder<PackagingType> builder)
    {
        builder.ToTable("packaging_types");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
    }
}

public class PackagingUnitConfiguration : IEntityTypeConfiguration<PackagingUnit>
{
    public void Configure(EntityTypeBuilder<PackagingUnit> builder)
    {
        builder.ToTable("packaging_units");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
    }
}

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("payment_methods");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
    }
}
