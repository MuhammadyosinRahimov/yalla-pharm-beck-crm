using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.DTOs;

public class PharmacyOrderDto
{
    public string? Id { get; set; }
    public string? OrderId { get; set; }
    public string? PharmacyId { get; set; }
    public PharmacyDto? Pharmacy { get; set; }
    public List<ProductHistoryDto>? ProductHistories { get; set; }
}

public class ProductHistoryDto
{
    public string? Id { get; set; }
    public string? ProductId { get; set; }
    public string? PharmacyOrderId { get; set; }
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
    public ProductDto? Product { get; set; }
}
