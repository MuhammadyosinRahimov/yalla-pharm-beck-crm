namespace YallaPharm.Domain.Entities;

public class Address
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ClientId { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Landmark { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? GeolocationOfClientAddress { get; set; }

    public virtual Client? Client { get; set; }
}
