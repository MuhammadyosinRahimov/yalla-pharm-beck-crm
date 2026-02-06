namespace YallaPharm.Application.DTOs;

public class ReferenceDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class PackagingTypeDto : ReferenceDto { }
public class PackagingUnitDto : ReferenceDto { }
public class PaymentMethodDto : ReferenceDto { }
public class ReleaseFormDto : ReferenceDto { }

// Product template reference data DTOs
public class CategoryDto : ReferenceDto
{
    public string? ParentId { get; set; }
}
public class ForWhomDto : ReferenceDto { }
public class ApplicationMethodDto : ReferenceDto { }
public class OrgansAndSystemDto : ReferenceDto { }
public class PackagingMaterialDto : ReferenceDto { }
public class PreparationColorDto : ReferenceDto { }
public class PreparationMaterialDto : ReferenceDto { }
public class ScopeOfApplicationDto : ReferenceDto { }
public class TimeOfApplicationDto : ReferenceDto { }
