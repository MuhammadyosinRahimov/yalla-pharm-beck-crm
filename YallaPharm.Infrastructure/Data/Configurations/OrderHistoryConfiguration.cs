using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("order_histories");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.OrderId).HasColumnName("order_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.Message).HasColumnName("message").HasMaxLength(1000);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at");
        builder.Property(e => e.IndividualDeliveryTime).HasColumnName("individual_delivery_time");
        builder.Property(e => e.State).HasColumnName("state").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.PaymentStatus).HasColumnName("payment_status").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.PaymentMethod).HasColumnName("payment_method").HasMaxLength(50);
        builder.Property(e => e.IsReturned).HasColumnName("is_returned").HasDefaultValue(false);
        builder.Property(e => e.RequestDate).HasColumnName("request_date");
        builder.Property(e => e.OrderDate).HasColumnName("order_date");
        builder.Property(e => e.TimeForAcceptingRequest).HasColumnName("time_for_accepting_request");
        builder.Property(e => e.TimeInformCustomerAboutProduct).HasColumnName("time_inform_customer_about_product");
        builder.Property(e => e.AmountOfTimeRespondClient).HasColumnName("amount_of_time_respond_client");
        builder.Property(e => e.TimeToObtainClientApproval).HasColumnName("time_to_obtain_client_approval");
        builder.Property(e => e.TimeToSendCheckToClient).HasColumnName("time_to_send_check_to_client");
        builder.Property(e => e.RequestProcessingTime).HasColumnName("request_processing_time");
        builder.Property(e => e.CommentForCourier).HasColumnName("comment_for_courier").HasMaxLength(5000);
        builder.Property(e => e.ReasonForOrderDelay).HasColumnName("reason_for_order_delay").HasMaxLength(5000);
        builder.Property(e => e.ReasonForOrderCancellation).HasColumnName("reason_for_order_cancellation").HasMaxLength(5000);
        builder.Property(e => e.ReasonForOrderRejection).HasColumnName("reason_for_order_rejection").HasMaxLength(5000);
        builder.Property(e => e.CallingAt).HasColumnName("calling_at").HasMaxLength(2000);
        builder.Property(e => e.LongSearchReason).HasColumnName("long_search_reason").HasMaxLength(5000);
        builder.Property(e => e.OrderProcessingTime).HasColumnName("order_processing_time");
        builder.Property(e => e.AmountOfProcessingTime).HasColumnName("amount_of_processing_time");
        builder.Property(e => e.AmountOfDeliveryTime).HasColumnName("amount_of_delivery_time");
        builder.Property(e => e.DeliveredTime).HasColumnName("delivered_time");
        builder.Property(e => e.TimeOfCompletionOfInquiry).HasColumnName("time_of_completion_of_inquiry");
        builder.Property(e => e.ReasonForReturningTheOrder).HasColumnName("reason_for_returning_the_order").HasMaxLength(1000);
        builder.Property(e => e.ReturnedProductsCount).HasColumnName("returned_products_count");
        builder.Property(e => e.WasRejection).HasColumnName("was_rejection").HasDefaultValue(false);
        builder.Property(e => e.PastState).HasColumnName("past_state").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.DeliveredAt).HasColumnName("delivered_at");
        builder.Property(e => e.CourierAcceptedAt).HasColumnName("courier_accepted_at");
        builder.Property(e => e.NotifiedAt).HasColumnName("notified_at");
        builder.Property(e => e.PlacedAt).HasColumnName("placed_at");
        builder.Property(e => e.ReadyForShipmentAt).HasColumnName("ready_for_shipment_at");
        builder.Property(e => e.SearchingAt).HasColumnName("searching_at");
        builder.Property(e => e.LeadCreatedAt).HasColumnName("lead_created_at");
        builder.Property(e => e.CourierReceivedAt).HasColumnName("courier_received_at");
        builder.Property(e => e.ConsultedAt).HasColumnName("consulted_at");
        builder.Property(e => e.PlacementAt).HasColumnName("placement_at");
        builder.Property(e => e.CanceledAt).HasColumnName("canceled_at");
        builder.Property(e => e.ConfirmedAt).HasColumnName("confirmed_at");
        builder.Property(e => e.PreviousStateBeforeRejectionState).HasColumnName("previous_state_before_rejection_state");
        builder.Property(e => e.MinutesForEntireProcessFinish).HasColumnName("minutes_for_entire_process_finish");
        builder.Property(e => e.MinutesForEntireProcessToPlacement).HasColumnName("minutes_for_entire_process_to_placement");

        builder.HasOne(e => e.Order)
            .WithOne(o => o.OrderHistory)
            .HasForeignKey<OrderHistory>(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.State).HasDatabaseName("idx_order_histories_state");
        builder.HasIndex(e => e.OrderId).HasDatabaseName("idx_order_histories_order_id").IsUnique();
    }
}
