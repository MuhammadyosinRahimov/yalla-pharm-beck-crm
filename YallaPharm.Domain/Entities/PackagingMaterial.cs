namespace YallaPharm.Domain.Entities;

public class PackagingMaterial
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PackagingMaterialValue { get; set; } = string.Empty;

    public virtual ICollection<ProductTemplate> ProductTemplates { get; set; } = new List<ProductTemplate>();
}
