using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IPackagingTypeService
{
    Task<PackagingTypeDto> CreateAsync(PackagingTypeDto dto);
    Task<PackagingTypeDto> UpdateAsync(PackagingTypeDto dto);
    Task DeleteAsync(string id);
    Task<PackagingTypeDto?> GetByIdAsync(string id);
    Task<IEnumerable<PackagingTypeDto>> GetAllAsync();
}

public interface IPackagingUnitService
{
    Task<PackagingUnitDto> CreateAsync(PackagingUnitDto dto);
    Task<PackagingUnitDto> UpdateAsync(PackagingUnitDto dto);
    Task DeleteAsync(string id);
    Task<PackagingUnitDto?> GetByIdAsync(string id);
    Task<IEnumerable<PackagingUnitDto>> GetAllAsync();
}

public interface IPaymentMethodService
{
    Task<PaymentMethodDto> CreateAsync(PaymentMethodDto dto);
    Task<PaymentMethodDto> UpdateAsync(PaymentMethodDto dto);
    Task DeleteAsync(string id);
    Task<PaymentMethodDto?> GetByIdAsync(string id);
    Task<IEnumerable<PaymentMethodDto>> GetAllAsync();
}

public interface IReleaseFormService
{
    Task<ReleaseFormDto> CreateAsync(ReleaseFormDto dto);
    Task<ReleaseFormDto> UpdateAsync(ReleaseFormDto dto);
    Task DeleteAsync(string id);
    Task<ReleaseFormDto?> GetByIdAsync(string id);
    Task<IEnumerable<ReleaseFormDto>> GetAllAsync();
}

// Product template reference services
public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CategoryDto dto);
    Task<CategoryDto> UpdateAsync(CategoryDto dto);
    Task DeleteAsync(string id);
    Task<CategoryDto?> GetByIdAsync(string id);
    Task<IEnumerable<CategoryDto>> GetAllAsync();
}

public interface IForWhomService
{
    Task<ForWhomDto> CreateAsync(ForWhomDto dto);
    Task<ForWhomDto> UpdateAsync(ForWhomDto dto);
    Task DeleteAsync(string id);
    Task<ForWhomDto?> GetByIdAsync(string id);
    Task<IEnumerable<ForWhomDto>> GetAllAsync();
}

public interface IApplicationMethodService
{
    Task<ApplicationMethodDto> CreateAsync(ApplicationMethodDto dto);
    Task<ApplicationMethodDto> UpdateAsync(ApplicationMethodDto dto);
    Task DeleteAsync(string id);
    Task<ApplicationMethodDto?> GetByIdAsync(string id);
    Task<IEnumerable<ApplicationMethodDto>> GetAllAsync();
}

public interface IOrgansAndSystemService
{
    Task<OrgansAndSystemDto> CreateAsync(OrgansAndSystemDto dto);
    Task<OrgansAndSystemDto> UpdateAsync(OrgansAndSystemDto dto);
    Task DeleteAsync(string id);
    Task<OrgansAndSystemDto?> GetByIdAsync(string id);
    Task<IEnumerable<OrgansAndSystemDto>> GetAllAsync();
}

public interface IPackagingMaterialService
{
    Task<PackagingMaterialDto> CreateAsync(PackagingMaterialDto dto);
    Task<PackagingMaterialDto> UpdateAsync(PackagingMaterialDto dto);
    Task DeleteAsync(string id);
    Task<PackagingMaterialDto?> GetByIdAsync(string id);
    Task<IEnumerable<PackagingMaterialDto>> GetAllAsync();
}

public interface IPreparationColorService
{
    Task<PreparationColorDto> CreateAsync(PreparationColorDto dto);
    Task<PreparationColorDto> UpdateAsync(PreparationColorDto dto);
    Task DeleteAsync(string id);
    Task<PreparationColorDto?> GetByIdAsync(string id);
    Task<IEnumerable<PreparationColorDto>> GetAllAsync();
}

public interface IPreparationMaterialService
{
    Task<PreparationMaterialDto> CreateAsync(PreparationMaterialDto dto);
    Task<PreparationMaterialDto> UpdateAsync(PreparationMaterialDto dto);
    Task DeleteAsync(string id);
    Task<PreparationMaterialDto?> GetByIdAsync(string id);
    Task<IEnumerable<PreparationMaterialDto>> GetAllAsync();
}

public interface IScopeOfApplicationService
{
    Task<ScopeOfApplicationDto> CreateAsync(ScopeOfApplicationDto dto);
    Task<ScopeOfApplicationDto> UpdateAsync(ScopeOfApplicationDto dto);
    Task DeleteAsync(string id);
    Task<ScopeOfApplicationDto?> GetByIdAsync(string id);
    Task<IEnumerable<ScopeOfApplicationDto>> GetAllAsync();
}

public interface ITimeOfApplicationService
{
    Task<TimeOfApplicationDto> CreateAsync(TimeOfApplicationDto dto);
    Task<TimeOfApplicationDto> UpdateAsync(TimeOfApplicationDto dto);
    Task DeleteAsync(string id);
    Task<TimeOfApplicationDto?> GetByIdAsync(string id);
    Task<IEnumerable<TimeOfApplicationDto>> GetAllAsync();
}
