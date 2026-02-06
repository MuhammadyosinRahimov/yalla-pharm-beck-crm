namespace YallaPharm.Domain.Entities;

public class PharmacyOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrderId { get; set; } = string.Empty;
    public string? PharmacyId { get; set; }

    public virtual Order? Order { get; set; }
    public virtual Pharmacy? Pharmacy { get; set; }
    public virtual ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();
}
