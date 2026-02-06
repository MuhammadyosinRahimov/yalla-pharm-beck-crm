using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

/// <summary>
/// Controller for product template reference data (categories, for whom, application methods, etc.)
/// GET endpoints are public, POST/DELETE require Administrator or SuperAdmin role
/// </summary>
[ApiController]
public class ReferenceDataApiController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IForWhomService _forWhomService;
    private readonly IApplicationMethodService _applicationMethodService;
    private readonly IOrgansAndSystemService _organsAndSystemService;
    private readonly IPackagingMaterialService _packagingMaterialService;
    private readonly IPreparationColorService _preparationColorService;
    private readonly IPreparationMaterialService _preparationMaterialService;
    private readonly IScopeOfApplicationService _scopeOfApplicationService;
    private readonly ITimeOfApplicationService _timeOfApplicationService;

    public ReferenceDataApiController(
        ICategoryService categoryService,
        IForWhomService forWhomService,
        IApplicationMethodService applicationMethodService,
        IOrgansAndSystemService organsAndSystemService,
        IPackagingMaterialService packagingMaterialService,
        IPreparationColorService preparationColorService,
        IPreparationMaterialService preparationMaterialService,
        IScopeOfApplicationService scopeOfApplicationService,
        ITimeOfApplicationService timeOfApplicationService)
    {
        _categoryService = categoryService;
        _forWhomService = forWhomService;
        _applicationMethodService = applicationMethodService;
        _organsAndSystemService = organsAndSystemService;
        _packagingMaterialService = packagingMaterialService;
        _preparationColorService = preparationColorService;
        _preparationMaterialService = preparationMaterialService;
        _scopeOfApplicationService = scopeOfApplicationService;
        _timeOfApplicationService = timeOfApplicationService;
    }

    // ==== CATEGORIES ====
    [HttpGet("api/category")]
    public async Task<IActionResult> GetAllCategories() => Ok(await _categoryService.GetAllAsync());

    [HttpGet("api/category/{id}")]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "Category not found" }) : Ok(result);
    }

    [HttpPost("api/category")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto) => Ok(await _categoryService.CreateAsync(dto));

    [HttpPost("api/category/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto dto)
    {
        try { return Ok(await _categoryService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/category/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok(new { message = "Category deleted successfully" });
    }

    // ==== FOR WHOM ====
    [HttpGet("api/forWhom")]
    public async Task<IActionResult> GetAllForWhom() => Ok(await _forWhomService.GetAllAsync());

    [HttpGet("api/forWhom/{id}")]
    public async Task<IActionResult> GetForWhomById(string id)
    {
        var result = await _forWhomService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "ForWhom not found" }) : Ok(result);
    }

    [HttpPost("api/forWhom")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateForWhom([FromBody] ForWhomDto dto) => Ok(await _forWhomService.CreateAsync(dto));

    [HttpPost("api/forWhom/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateForWhom([FromBody] ForWhomDto dto)
    {
        try { return Ok(await _forWhomService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/forWhom/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteForWhom(string id)
    {
        await _forWhomService.DeleteAsync(id);
        return Ok(new { message = "ForWhom deleted successfully" });
    }

    // ==== APPLICATION METHOD ====
    [HttpGet("api/applicationMethod")]
    public async Task<IActionResult> GetAllApplicationMethods() => Ok(await _applicationMethodService.GetAllAsync());

    [HttpGet("api/applicationMethod/{id}")]
    public async Task<IActionResult> GetApplicationMethodById(string id)
    {
        var result = await _applicationMethodService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "ApplicationMethod not found" }) : Ok(result);
    }

    [HttpPost("api/applicationMethod")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateApplicationMethod([FromBody] ApplicationMethodDto dto) => Ok(await _applicationMethodService.CreateAsync(dto));

    [HttpPost("api/applicationMethod/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateApplicationMethod([FromBody] ApplicationMethodDto dto)
    {
        try { return Ok(await _applicationMethodService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/applicationMethod/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteApplicationMethod(string id)
    {
        await _applicationMethodService.DeleteAsync(id);
        return Ok(new { message = "ApplicationMethod deleted successfully" });
    }

    // ==== ORGANS AND SYSTEMS ====
    [HttpGet("api/organsAndSystem")]
    public async Task<IActionResult> GetAllOrgansAndSystems() => Ok(await _organsAndSystemService.GetAllAsync());

    [HttpGet("api/organsAndSystem/{id}")]
    public async Task<IActionResult> GetOrgansAndSystemById(string id)
    {
        var result = await _organsAndSystemService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "OrgansAndSystem not found" }) : Ok(result);
    }

    [HttpPost("api/organsAndSystem")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateOrgansAndSystem([FromBody] OrgansAndSystemDto dto) => Ok(await _organsAndSystemService.CreateAsync(dto));

    [HttpPost("api/organsAndSystem/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateOrgansAndSystem([FromBody] OrgansAndSystemDto dto)
    {
        try { return Ok(await _organsAndSystemService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/organsAndSystem/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteOrgansAndSystem(string id)
    {
        await _organsAndSystemService.DeleteAsync(id);
        return Ok(new { message = "OrgansAndSystem deleted successfully" });
    }

    // ==== PACKAGING MATERIAL ====
    [HttpGet("api/packagingMaterial")]
    public async Task<IActionResult> GetAllPackagingMaterials() => Ok(await _packagingMaterialService.GetAllAsync());

    [HttpGet("api/packagingMaterial/{id}")]
    public async Task<IActionResult> GetPackagingMaterialById(string id)
    {
        var result = await _packagingMaterialService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "PackagingMaterial not found" }) : Ok(result);
    }

    [HttpPost("api/packagingMaterial")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreatePackagingMaterial([FromBody] PackagingMaterialDto dto) => Ok(await _packagingMaterialService.CreateAsync(dto));

    [HttpPost("api/packagingMaterial/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdatePackagingMaterial([FromBody] PackagingMaterialDto dto)
    {
        try { return Ok(await _packagingMaterialService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/packagingMaterial/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeletePackagingMaterial(string id)
    {
        await _packagingMaterialService.DeleteAsync(id);
        return Ok(new { message = "PackagingMaterial deleted successfully" });
    }

    // ==== PREPARATION COLOR ====
    [HttpGet("api/preparationColor")]
    public async Task<IActionResult> GetAllPreparationColors() => Ok(await _preparationColorService.GetAllAsync());

    [HttpGet("api/preparationColor/{id}")]
    public async Task<IActionResult> GetPreparationColorById(string id)
    {
        var result = await _preparationColorService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "PreparationColor not found" }) : Ok(result);
    }

    [HttpPost("api/preparationColor")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreatePreparationColor([FromBody] PreparationColorDto dto) => Ok(await _preparationColorService.CreateAsync(dto));

    [HttpPost("api/preparationColor/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdatePreparationColor([FromBody] PreparationColorDto dto)
    {
        try { return Ok(await _preparationColorService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/preparationColor/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeletePreparationColor(string id)
    {
        await _preparationColorService.DeleteAsync(id);
        return Ok(new { message = "PreparationColor deleted successfully" });
    }

    // ==== PREPARATION MATERIAL ====
    [HttpGet("api/preparationMaterial")]
    public async Task<IActionResult> GetAllPreparationMaterials() => Ok(await _preparationMaterialService.GetAllAsync());

    [HttpGet("api/preparationMaterial/{id}")]
    public async Task<IActionResult> GetPreparationMaterialById(string id)
    {
        var result = await _preparationMaterialService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "PreparationMaterial not found" }) : Ok(result);
    }

    [HttpPost("api/preparationMaterial")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreatePreparationMaterial([FromBody] PreparationMaterialDto dto) => Ok(await _preparationMaterialService.CreateAsync(dto));

    [HttpPost("api/preparationMaterial/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdatePreparationMaterial([FromBody] PreparationMaterialDto dto)
    {
        try { return Ok(await _preparationMaterialService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/preparationMaterial/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeletePreparationMaterial(string id)
    {
        await _preparationMaterialService.DeleteAsync(id);
        return Ok(new { message = "PreparationMaterial deleted successfully" });
    }

    // ==== SCOPE OF APPLICATION ====
    [HttpGet("api/scopeOfApplication")]
    public async Task<IActionResult> GetAllScopeOfApplications() => Ok(await _scopeOfApplicationService.GetAllAsync());

    [HttpGet("api/scopeOfApplication/{id}")]
    public async Task<IActionResult> GetScopeOfApplicationById(string id)
    {
        var result = await _scopeOfApplicationService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "ScopeOfApplication not found" }) : Ok(result);
    }

    [HttpPost("api/scopeOfApplication")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateScopeOfApplication([FromBody] ScopeOfApplicationDto dto) => Ok(await _scopeOfApplicationService.CreateAsync(dto));

    [HttpPost("api/scopeOfApplication/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateScopeOfApplication([FromBody] ScopeOfApplicationDto dto)
    {
        try { return Ok(await _scopeOfApplicationService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/scopeOfApplication/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteScopeOfApplication(string id)
    {
        await _scopeOfApplicationService.DeleteAsync(id);
        return Ok(new { message = "ScopeOfApplication deleted successfully" });
    }

    // ==== TIME OF APPLICATION ====
    [HttpGet("api/timeOfApplication")]
    public async Task<IActionResult> GetAllTimeOfApplications() => Ok(await _timeOfApplicationService.GetAllAsync());

    [HttpGet("api/timeOfApplication/{id}")]
    public async Task<IActionResult> GetTimeOfApplicationById(string id)
    {
        var result = await _timeOfApplicationService.GetByIdAsync(id);
        return result == null ? NotFound(new { message = "TimeOfApplication not found" }) : Ok(result);
    }

    [HttpPost("api/timeOfApplication")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> CreateTimeOfApplication([FromBody] TimeOfApplicationDto dto) => Ok(await _timeOfApplicationService.CreateAsync(dto));

    [HttpPost("api/timeOfApplication/update")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> UpdateTimeOfApplication([FromBody] TimeOfApplicationDto dto)
    {
        try { return Ok(await _timeOfApplicationService.UpdateAsync(dto)); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpDelete("api/timeOfApplication/{id}")]
    [Authorize(Roles = "Administrator,SuperAdmin")]
    public async Task<IActionResult> DeleteTimeOfApplication(string id)
    {
        await _timeOfApplicationService.DeleteAsync(id);
        return Ok(new { message = "TimeOfApplication deleted successfully" });
    }
}
