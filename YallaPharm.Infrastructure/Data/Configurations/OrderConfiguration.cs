using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ClientId).HasColumnName("client_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.OrderNumber).HasColumnName("order_number").HasMaxLength(50).IsRequired();
        builder.Property(e => e.Comment).HasColumnName("comment").HasMaxLength(5000);
        builder.Property(e => e.CountryToDelivery).HasColumnName("country_to_delivery").HasMaxLength(100);
        builder.Property(e => e.Courier).HasColumnName("courier").HasMaxLength(100);
        builder.Property(e => e.CommentForCourier).HasColumnName("comment_for_courier").HasMaxLength(500);
        builder.Property(e => e.Operator).HasColumnName("operator").HasMaxLength(100).IsRequired();
        builder.Property(e => e.TotalCost).HasColumnName("total_cost").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.Prepayment).HasColumnName("prepayment").HasColumnType("decimal(18,2)");
        builder.Property(e => e.RestPayment).HasColumnName("rest_payment").HasColumnType("decimal(18,2)");
        builder.Property(e => e.Discount).HasColumnName("discount").HasColumnType("decimal(18,2)");
        builder.Property(e => e.TotalOrderAmountExcludingDelivery).HasColumnName("total_order_amount_excluding_delivery").HasColumnType("decimal(18,2)");
        builder.Property(e => e.CityOrDistrict).HasColumnName("city_or_district").HasMaxLength(100).IsRequired();
        builder.Property(e => e.PriceForDeliveryOutsideTheCity).HasColumnName("price_for_delivery_outside_the_city").HasColumnType("decimal(18,2)");
        builder.Property(e => e.RemainingPayment).HasColumnName("remaining_payment").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithDiscount).HasColumnName("amount_with_discount").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithMarkup).HasColumnName("amount_with_markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithoutMarkup).HasColumnName("amount_without_markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithDelivery).HasColumnName("amount_with_delivery").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithoutDelivery).HasColumnName("amount_without_delivery").HasColumnType("decimal(18,2)");
        builder.Property(e => e.Type).HasColumnName("type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.ComesFrom).HasColumnName("comes_from").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.DeliveryType).HasColumnName("delivery_type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.DeliveredAt).HasColumnName("delivered_at").HasMaxLength(50);

        builder.HasOne(e => e.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.ClientId).HasDatabaseName("idx_orders_client_id");
        builder.HasIndex(e => e.OrderNumber).HasDatabaseName("idx_orders_order_number");
    }
}
