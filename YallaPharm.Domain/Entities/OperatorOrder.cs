namespace YallaPharm.Domain.Entities;

public class OperatorOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OperatorId { get; set; } = string.Empty;
    public string? OrderOperatorId { get; set; }
    public string? OrderHistoryId { get; set; }

    public virtual User? Operator { get; set; }
    public virtual User? OrderOperator { get; set; }
    public virtual OrderHistory? OrderHistory { get; set; }
}
