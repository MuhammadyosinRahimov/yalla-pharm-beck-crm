using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public int? CountOnPackage { get; set; }
    public int? AgeFrom { get; set; }
    public int? AgeTo { get; set; }
    public decimal PriceWithMarkup { get; set; }
    public decimal? PriceWithoutMarkup { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public string PathImage { get; set; } = string.Empty;
    public bool Dificit { get; set; }
    public string ReleaseForm { get; set; } = string.Empty;
    public string PackagingUnit { get; set; } = string.Empty;
    public string TypeOfPackaging { get; set; } = string.Empty;
    public string LinkProduct { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public AgeType? AgeType { get; set; }
    public bool IsRequired { get; set; }
    public ProductState? State { get; set; }

    public virtual ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();
    public virtual ICollection<ProductProvider> ProductProviders { get; set; } = new List<ProductProvider>();
}
