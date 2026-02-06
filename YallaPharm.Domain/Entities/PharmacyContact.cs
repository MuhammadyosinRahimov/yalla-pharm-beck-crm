using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class PharmacyContact
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PharmacyId { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public ContactType? ContactType { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }
}
