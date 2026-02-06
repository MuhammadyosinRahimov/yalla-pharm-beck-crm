using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class Pharmacy
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Landmark { get; set; }
    public string? Contact { get; set; }
    public string? GeolocationLink { get; set; }
    public string? Country { get; set; }
    public decimal? Markup { get; set; }
    public MarkupType? MarkupType { get; set; }
    public bool IsRequired { get; set; }
    public bool IsAbroad { get; set; }
    public PharmacyPayoutMethod? PayoutMethod { get; set; }

    public virtual ICollection<PharmacyOrder> PharmacyOrders { get; set; } = new List<PharmacyOrder>();
    public virtual ICollection<PharmacyContact> PharmacyContacts { get; set; } = new List<PharmacyContact>();
}
