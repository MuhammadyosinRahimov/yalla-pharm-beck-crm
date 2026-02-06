namespace YallaPharm.Domain.Entities;

public class PackagingType
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
}
