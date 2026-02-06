namespace YallaPharm.Domain.Entities;

public class Country
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CountryName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;

    public virtual Order? Order { get; set; }
}
