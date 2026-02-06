using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ClientId { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string? CountryToDelivery { get; set; }
    public string? Courier { get; set; }
    public string? CommentForCourier { get; set; }
    public string Operator { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public decimal? Prepayment { get; set; }
    public decimal? RestPayment { get; set; }
    public decimal? Discount { get; set; }
    public decimal? TotalOrderAmountExcludingDelivery { get; set; }
    public string CityOrDistrict { get; set; } = string.Empty;
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

    public virtual Client? Client { get; set; }
    public virtual OrderHistory? OrderHistory { get; set; }
    public virtual ICollection<PharmacyOrder> PharmacyOrders { get; set; } = new List<PharmacyOrder>();
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
