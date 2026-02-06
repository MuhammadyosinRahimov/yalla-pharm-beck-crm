namespace YallaPharm.Domain.Entities;

public class PreparationMaterial
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PreparationMaterialValue { get; set; } = string.Empty;

    public virtual ICollection<ProductTemplate> ProductTemplates { get; set; } = new List<ProductTemplate>();
}
