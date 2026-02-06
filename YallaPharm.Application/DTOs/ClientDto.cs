using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.DTOs;

public class ClientDto
{
    public string? Id { get; set; }
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
    public List<AddressDto>? Addresses { get; set; }
    public List<ClientChildrenDto>? Childrens { get; set; }
    public List<ClientAdultDto>? Adults { get; set; }
}

public class AddressDto
{
    public string? Id { get; set; }
    public string? ClientId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Landmark { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? GeolocationOfClientAddress { get; set; }
}

public class ClientChildrenDto
{
    public string? Id { get; set; }
    public string? ClientId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public short? Age { get; set; }
    public Gender? Gender { get; set; }
}

public class ClientAdultDto
{
    public string? Id { get; set; }
    public string? ClientId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public short? Age { get; set; }
    public Gender? Gender { get; set; }
}
