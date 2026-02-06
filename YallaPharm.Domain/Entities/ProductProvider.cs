namespace YallaPharm.Domain.Entities;

public class ProductProvider
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; } = string.Empty;
    public string ProviderId { get; set; } = string.Empty;

    public virtual Product? Product { get; set; }
    public virtual Provider? Provider { get; set; }
}
