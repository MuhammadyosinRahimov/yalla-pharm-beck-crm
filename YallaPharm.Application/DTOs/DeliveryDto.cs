namespace YallaPharm.Application.DTOs;

public class DeliverySendDto
{
    public string OrderId { get; set; } = string.Empty;
    public string? CourierId { get; set; }
    public string? Comment { get; set; }
}

public class DeliveryUpdateDto
{
    public string OrderId { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? Comment { get; set; }
}
