using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.DTOs;

public class PharmacyDto
{
    public string? Id { get; set; }
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
    public List<PharmacyContactDto>? PharmacyContacts { get; set; }
}

public class PharmacyContactDto
{
    public string? Id { get; set; }
    public string? PharmacyId { get; set; }
    public string Contact { get; set; } = string.Empty;
    public ContactType? ContactType { get; set; }
}
