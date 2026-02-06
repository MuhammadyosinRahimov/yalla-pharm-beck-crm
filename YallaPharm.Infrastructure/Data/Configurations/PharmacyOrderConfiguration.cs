using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class PharmacyOrderConfiguration : IEntityTypeConfiguration<PharmacyOrder>
{
    public void Configure(EntityTypeBuilder<PharmacyOrder> builder)
    {
        builder.ToTable("pharmacy_orders");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.OrderId).HasColumnName("order_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.PharmacyId).HasColumnName("pharmacy_id").HasMaxLength(36);

        builder.HasOne(e => e.Order)
            .WithMany(o => o.PharmacyOrders)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Pharmacy)
            .WithMany(p => p.PharmacyOrders)
            .HasForeignKey(e => e.PharmacyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
