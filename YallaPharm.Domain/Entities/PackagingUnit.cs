namespace YallaPharm.Domain.Entities;

public class PackagingUnit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
}
