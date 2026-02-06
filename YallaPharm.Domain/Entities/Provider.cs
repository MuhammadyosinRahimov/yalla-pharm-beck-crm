using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class Provider
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public ContactType? ContactType { get; set; }
    public string Contact { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string LinkFromWhereFoundAbroad { get; set; } = string.Empty;

    public virtual ICollection<ProductProvider> ProductProviders { get; set; } = new List<ProductProvider>();
}
