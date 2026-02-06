namespace YallaPharm.Domain.Entities;

public class ProductTemplate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Nickname { get; set; }
    public string? ActiveIngredient { get; set; }
    public string? Dosage { get; set; }
    public string? DosageUnit { get; set; }
    public int? MinimumQuantityPerPiece { get; set; }
    public string PackQuantityOrDrugVolume { get; set; } = string.Empty;
    public string? PackagingUnit { get; set; }
    public string? PreparationTaste { get; set; }
    public short? AgeFrom { get; set; }
    public short? AgeTo { get; set; }
    public string? Instructions { get; set; }
    public string IndicationForUse { get; set; } = string.Empty;
    public string ContraindicationForUse { get; set; } = string.Empty;
    public string? Symptom { get; set; }
    public string? ForAllergySufferers { get; set; }
    public string? ForDiabetics { get; set; }
    public string? ForPregnantWomen { get; set; }
    public string? ForChildren { get; set; }
    public string? ForDriver { get; set; }
    public string? SeasonOfApplication { get; set; }
    public string? DriedFruit { get; set; }
    public string? Comment { get; set; }
    public string? WithCaution { get; set; }
    public string? VacationCondition { get; set; }
    public string ForWhomId { get; set; } = string.Empty;
    public string ApplicationMethodId { get; set; } = string.Empty;
    public string? OrgansAndSystemsId { get; set; }
    public string PackagingMaterialId { get; set; } = string.Empty;
    public string PreparationColorId { get; set; } = string.Empty;
    public string PreparationMaterialId { get; set; } = string.Empty;
    public string? ScopeOfApplicationId { get; set; }
    public string TimeOfApplicationId { get; set; } = string.Empty;

    public virtual Category? Category { get; set; }
    public virtual ForWhom? ForWhom { get; set; }
    public virtual ApplicationMethod? ApplicationMethod { get; set; }
    public virtual OrgansAndSystem? OrgansAndSystems { get; set; }
    public virtual PackagingMaterial? PackagingMaterial { get; set; }
    public virtual PreparationColor? PreparationColor { get; set; }
    public virtual PreparationMaterial? PreparationMaterial { get; set; }
    public virtual ScopeOfApplication? ScopeOfApplication { get; set; }
    public virtual TimeOfApplication? TimeOfApplication { get; set; }
}
