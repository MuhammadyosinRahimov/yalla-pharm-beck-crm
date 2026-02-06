using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class ProductHistory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? ProductId { get; set; }
    public string PharmacyOrderId { get; set; } = string.Empty;
    public string? Message { get; set; }
    public string? LongSearchReason { get; set; }
    public string? ReturnReason { get; set; }
    public bool IsReturned { get; set; }
    public int? ReturnedCount { get; set; }
    public short? Count { get; set; }
    public decimal? AmountWithMarkup { get; set; }
    public decimal? AmountWithoutMarkup { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public ReturnedType? ReturnTo { get; set; }
    public string? Comment { get; set; }

    public virtual Product? Product { get; set; }
    public virtual PharmacyOrder? PharmacyOrder { get; set; }
}
