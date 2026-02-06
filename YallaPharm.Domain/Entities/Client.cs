using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class Client
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; } = string.Empty;
    public string? Street { get; set; }
    public string? Landmark { get; set; }
    public string? GeolocationOfClientAddress { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public short? Age { get; set; }
    public string? SocialUsername { get; set; }
    public Language? Language { get; set; }
    public bool HavingChildren { get; set; }
    public bool HavingElderly { get; set; }
    public Gender? Gender { get; set; }
    public FamilyStatus? FamilyStatus { get; set; }
    public EconomicStanding? EconomicStanding { get; set; }
    public string? ChildrensAge { get; set; }
    public TypeOfClient? Type { get; set; }
    public ContactType? ContactType { get; set; }
    public string? TakeIntoAccount { get; set; }
    public int DiscountForClient { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public virtual ICollection<ClientChildren> Childrens { get; set; } = new List<ClientChildren>();
    public virtual ICollection<ClientAdult> Adults { get; set; } = new List<ClientAdult>();
}
