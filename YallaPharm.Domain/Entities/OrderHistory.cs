using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class OrderHistory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrderId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? IndividualDeliveryTime { get; set; }
    public OrderState? State { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
    public string? PaymentMethod { get; set; }
    public bool IsReturned { get; set; }
    public DateTime? RequestDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? TimeForAcceptingRequest { get; set; }
    public DateTime? TimeInformCustomerAboutProduct { get; set; }
    public int? AmountOfTimeRespondClient { get; set; }
    public DateTime? TimeToObtainClientApproval { get; set; }
    public DateTime? TimeToSendCheckToClient { get; set; }
    public DateTime? RequestProcessingTime { get; set; }
    public string? CommentForCourier { get; set; }
    public string? ReasonForOrderDelay { get; set; }
    public string? ReasonForOrderCancellation { get; set; }
    public string? ReasonForOrderRejection { get; set; }
    public string? CallingAt { get; set; }
    public string? LongSearchReason { get; set; }
    public DateTime? OrderProcessingTime { get; set; }
    public int? AmountOfProcessingTime { get; set; }
    public int? AmountOfDeliveryTime { get; set; }
    public DateTime? DeliveredTime { get; set; }
    public DateTime? TimeOfCompletionOfInquiry { get; set; }
    public string? ReasonForReturningTheOrder { get; set; }
    public int? ReturnedProductsCount { get; set; }
    public bool WasRejection { get; set; }
    public OrderState? PastState { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CourierAcceptedAt { get; set; }
    public DateTime? NotifiedAt { get; set; }
    public DateTime? PlacedAt { get; set; }
    public DateTime? ReadyForShipmentAt { get; set; }
    public DateTime? SearchingAt { get; set; }
    public DateTime? LeadCreatedAt { get; set; }
    public DateTime? CourierReceivedAt { get; set; }
    public DateTime? ConsultedAt { get; set; }
    public DateTime? PlacementAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public int? PreviousStateBeforeRejectionState { get; set; }
    public int? MinutesForEntireProcessFinish { get; set; }
    public int? MinutesForEntireProcessToPlacement { get; set; }

    public virtual Order? Order { get; set; }
    public virtual ICollection<CourierOrder> CourierOrders { get; set; } = new List<CourierOrder>();
    public virtual ICollection<OperatorOrder> OperatorOrders { get; set; } = new List<OperatorOrder>();
}
