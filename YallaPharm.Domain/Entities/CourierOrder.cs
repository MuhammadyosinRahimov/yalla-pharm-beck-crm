namespace YallaPharm.Domain.Entities;

public class CourierOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CourierId { get; set; } = string.Empty;
    public string? OrderHistoryId { get; set; }

    public virtual User? Courier { get; set; }
    public virtual OrderHistory? OrderHistory { get; set; }
}
