using Microsoft.AspNetCore.Http;

namespace YallaPharm.Application.DTOs;

public class ProductTemplateDto
{
    public string? Id { get; set; }
    public string? CategoryId { get; set; }
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
    public string? ForWhomId { get; set; }
    public string? ApplicationMethodId { get; set; }
    public string? OrgansAndSystemsId { get; set; }
    public string? PackagingMaterialId { get; set; }
    public string? PreparationColorId { get; set; }
    public string? PreparationMaterialId { get; set; }
    public string? ScopeOfApplicationId { get; set; }
    public string? TimeOfApplicationId { get; set; }
}

public class ProductTemplateImageDto
{
    public string ProductTemplateId { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}
