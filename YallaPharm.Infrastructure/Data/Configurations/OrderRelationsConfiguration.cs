using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class CourierOrderConfiguration : IEntityTypeConfiguration<CourierOrder>
{
    public void Configure(EntityTypeBuilder<CourierOrder> builder)
    {
        builder.ToTable("courier_orders");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.CourierId).HasColumnName("courier_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.OrderHistoryId).HasColumnName("order_history_id").HasMaxLength(36);

        builder.HasOne(e => e.Courier)
            .WithMany(u => u.CourierOrders)
            .HasForeignKey(e => e.CourierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.OrderHistory)
            .WithMany(oh => oh.CourierOrders)
            .HasForeignKey(e => e.OrderHistoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class OperatorOrderConfiguration : IEntityTypeConfiguration<OperatorOrder>
{
    public void Configure(EntityTypeBuilder<OperatorOrder> builder)
    {
        builder.ToTable("operator_orders");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.OperatorId).HasColumnName("operator_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.OrderOperatorId).HasColumnName("order_operator_id").HasMaxLength(36);
        builder.Property(e => e.OrderHistoryId).HasColumnName("order_history_id").HasMaxLength(36);

        builder.HasOne(e => e.Operator)
            .WithMany(u => u.OperatorOrders)
            .HasForeignKey(e => e.OperatorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.OrderOperator)
            .WithMany()
            .HasForeignKey(e => e.OrderOperatorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.OrderHistory)
            .WithMany(oh => oh.OperatorOrders)
            .HasForeignKey(e => e.OrderHistoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.CountryName).HasColumnName("country").HasMaxLength(100).IsRequired();
        builder.Property(e => e.City).HasColumnName("city").HasMaxLength(100).IsRequired();
        builder.Property(e => e.OrderId).HasColumnName("order_id").HasMaxLength(36).IsRequired();

        builder.HasOne(e => e.Order)
            .WithMany(o => o.Countries)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
