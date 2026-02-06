using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ProductHistoryConfiguration : IEntityTypeConfiguration<ProductHistory>
{
    public void Configure(EntityTypeBuilder<ProductHistory> builder)
    {
        builder.ToTable("product_histories");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ProductId).HasColumnName("product_id").HasMaxLength(36);
        builder.Property(e => e.PharmacyOrderId).HasColumnName("pharmacy_order_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.Message).HasColumnName("message").HasMaxLength(5000);
        builder.Property(e => e.LongSearchReason).HasColumnName("long_search_reason").HasMaxLength(5000);
        builder.Property(e => e.ReturnReason).HasColumnName("return_reason").HasMaxLength(5000);
        builder.Property(e => e.IsReturned).HasColumnName("is_returned").HasDefaultValue(false);
        builder.Property(e => e.ReturnedCount).HasColumnName("returned_count");
        builder.Property(e => e.Count).HasColumnName("count");
        builder.Property(e => e.AmountWithMarkup).HasColumnName("amount_with_markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.AmountWithoutMarkup).HasColumnName("amount_without_markup").HasColumnType("decimal(18,2)");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.ArrivalDate).HasColumnName("arrival_date");
        builder.Property(e => e.ReturnTo).HasColumnName("return_to").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.Comment).HasColumnName("comment").HasMaxLength(5000);

        builder.HasOne(e => e.Product)
            .WithMany(p => p.ProductHistories)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PharmacyOrder)
            .WithMany(po => po.ProductHistories)
            .HasForeignKey(e => e.PharmacyOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
