namespace YallaPharm.Domain.Entities;

public class TimeOfApplication
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TimeOfApplicationValue { get; set; } = string.Empty;

    public virtual ICollection<ProductTemplate> ProductTemplates { get; set; } = new List<ProductTemplate>();
}
