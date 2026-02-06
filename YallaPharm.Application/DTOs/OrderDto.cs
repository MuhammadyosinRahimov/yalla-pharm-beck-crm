using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.DTOs;

public class OrderDto
{
    public string? Id { get; set; }
    public string? ClientId { get; set; }
    public string? OrderNumber { get; set; }
    public string? Comment { get; set; }
    public string? CountryToDelivery { get; set; }
    public string? Courier { get; set; }
    public string? CommentForCourier { get; set; }
    public string? Operator { get; set; }
    public decimal TotalCost { get; set; }
    public decimal? Prepayment { get; set; }
    public decimal? RestPayment { get; set; }
    public decimal? Discount { get; set; }
    public decimal? TotalOrderAmountExcludingDelivery { get; set; }
    public string? CityOrDistrict { get; set; }
    public decimal? PriceForDeliveryOutsideTheCity { get; set; }
    public decimal? RemainingPayment { get; set; }
    public decimal? AmountWithDiscount { get; set; }
    public decimal? AmountWithMarkup { get; set; }
    public decimal? AmountWithoutMarkup { get; set; }
    public decimal? AmountWithDelivery { get; set; }
    public decimal? AmountWithoutDelivery { get; set; }
    public OrderType? Type { get; set; }
    public OrderComesFrom? ComesFrom { get; set; }
    public DeliveryType? DeliveryType { get; set; }
    public string? DeliveredAt { get; set; }
    public ClientDto? Client { get; set; }
    public OrderHistoryDto? OrderHistory { get; set; }
    public List<PharmacyOrderDto>? PharmacyOrders { get; set; }
}

public class OrderHistoryDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
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
}

public class UpdateOrderStateDto
{
    public string OrderId { get; set; } = string.Empty;
    public OrderState State { get; set; }
    public string? Comment { get; set; }
    public string? Courier { get; set; }
    public string? CommentForCourier { get; set; }
    public string? ReasonForOrderDelay { get; set; }
    public string? ReasonForOrderCancellation { get; set; }
    public string? ReasonForOrderRejection { get; set; }
    public string? ReasonForReturningTheOrder { get; set; }
    public string? LongSearchReason { get; set; }
    public string? CallingAt { get; set; }
    public DateTime? IndividualDeliveryTime { get; set; }
    public string? PaymentMethod { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
}

public class OrdersByStateFilterDto
{
    public OrderState OrderState { get; set; }
    public int Count { get; set; }
    public string? OrderOrClientInfo { get; set; }
}
