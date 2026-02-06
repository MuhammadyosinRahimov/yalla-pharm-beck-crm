namespace YallaPharm.Domain.Entities;

public class ScopeOfApplication
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ScopeOfApplicationValue { get; set; } = string.Empty;

    public virtual ICollection<ProductTemplate> ProductTemplates { get; set; } = new List<ProductTemplate>();
}
