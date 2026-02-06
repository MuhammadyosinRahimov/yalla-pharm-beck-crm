using Microsoft.EntityFrameworkCore;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

public class PackagingTypeService : IPackagingTypeService
{
    private readonly ApplicationDbContext _context;

    public PackagingTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PackagingTypeDto> CreateAsync(PackagingTypeDto dto)
    {
        var entity = new PackagingType { Id = Guid.NewGuid().ToString(), Name = dto.Name };
        _context.PackagingTypes.Add(entity);
        await _context.SaveChangesAsync();
        return new PackagingTypeDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<PackagingTypeDto> UpdateAsync(PackagingTypeDto dto)
    {
        var entity = await _context.PackagingTypes.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PackagingType with ID {dto.Id} not found");
        entity.Name = dto.Name;
        await _context.SaveChangesAsync();
        return new PackagingTypeDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PackagingTypes.FindAsync(id);
        if (entity != null) { _context.PackagingTypes.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PackagingTypeDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PackagingTypes.FindAsync(id);
        return entity == null ? null : new PackagingTypeDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<IEnumerable<PackagingTypeDto>> GetAllAsync()
    {
        var entities = await _context.PackagingTypes.ToListAsync();
        return entities.Select(e => new PackagingTypeDto { Id = e.Id, Name = e.Name });
    }
}

public class PackagingUnitService : IPackagingUnitService
{
    private readonly ApplicationDbContext _context;

    public PackagingUnitService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PackagingUnitDto> CreateAsync(PackagingUnitDto dto)
    {
        var entity = new PackagingUnit { Id = Guid.NewGuid().ToString(), Name = dto.Name };
        _context.PackagingUnits.Add(entity);
        await _context.SaveChangesAsync();
        return new PackagingUnitDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<PackagingUnitDto> UpdateAsync(PackagingUnitDto dto)
    {
        var entity = await _context.PackagingUnits.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PackagingUnit with ID {dto.Id} not found");
        entity.Name = dto.Name;
        await _context.SaveChangesAsync();
        return new PackagingUnitDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PackagingUnits.FindAsync(id);
        if (entity != null) { _context.PackagingUnits.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PackagingUnitDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PackagingUnits.FindAsync(id);
        return entity == null ? null : new PackagingUnitDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<IEnumerable<PackagingUnitDto>> GetAllAsync()
    {
        var entities = await _context.PackagingUnits.ToListAsync();
        return entities.Select(e => new PackagingUnitDto { Id = e.Id, Name = e.Name });
    }
}

public class PaymentMethodService : IPaymentMethodService
{
    private readonly ApplicationDbContext _context;

    public PaymentMethodService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethodDto> CreateAsync(PaymentMethodDto dto)
    {
        var entity = new PaymentMethod { Id = Guid.NewGuid().ToString(), Name = dto.Name };
        _context.PaymentMethods.Add(entity);
        await _context.SaveChangesAsync();
        return new PaymentMethodDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<PaymentMethodDto> UpdateAsync(PaymentMethodDto dto)
    {
        var entity = await _context.PaymentMethods.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PaymentMethod with ID {dto.Id} not found");
        entity.Name = dto.Name;
        await _context.SaveChangesAsync();
        return new PaymentMethodDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PaymentMethods.FindAsync(id);
        if (entity != null) { _context.PaymentMethods.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PaymentMethodDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PaymentMethods.FindAsync(id);
        return entity == null ? null : new PaymentMethodDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync()
    {
        var entities = await _context.PaymentMethods.ToListAsync();
        return entities.Select(e => new PaymentMethodDto { Id = e.Id, Name = e.Name });
    }
}

public class ReleaseFormService : IReleaseFormService
{
    private readonly ApplicationDbContext _context;

    public ReleaseFormService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReleaseFormDto> CreateAsync(ReleaseFormDto dto)
    {
        var entity = new ReleaseForm { Id = Guid.NewGuid().ToString(), Name = dto.Name };
        _context.ReleaseForms.Add(entity);
        await _context.SaveChangesAsync();
        return new ReleaseFormDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<ReleaseFormDto> UpdateAsync(ReleaseFormDto dto)
    {
        var entity = await _context.ReleaseForms.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"ReleaseForm with ID {dto.Id} not found");
        entity.Name = dto.Name;
        await _context.SaveChangesAsync();
        return new ReleaseFormDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.ReleaseForms.FindAsync(id);
        if (entity != null) { _context.ReleaseForms.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<ReleaseFormDto?> GetByIdAsync(string id)
    {
        var entity = await _context.ReleaseForms.FindAsync(id);
        return entity == null ? null : new ReleaseFormDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<IEnumerable<ReleaseFormDto>> GetAllAsync()
    {
        var entities = await _context.ReleaseForms.ToListAsync();
        return entities.Select(e => new ReleaseFormDto { Id = e.Id, Name = e.Name });
    }
}

// ==== PRODUCT TEMPLATE REFERENCE SERVICES ====

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    public CategoryService(ApplicationDbContext context) => _context = context;

    public async Task<CategoryDto> CreateAsync(CategoryDto dto)
    {
        var entity = new Category { Id = Guid.NewGuid().ToString(), Name = dto.Name, ParentId = dto.ParentId };
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return new CategoryDto { Id = entity.Id, Name = entity.Name, ParentId = entity.ParentId };
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto dto)
    {
        var entity = await _context.Categories.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"Category with ID {dto.Id} not found");
        entity.Name = dto.Name;
        entity.ParentId = dto.ParentId;
        await _context.SaveChangesAsync();
        return new CategoryDto { Id = entity.Id, Name = entity.Name, ParentId = entity.ParentId };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity != null) { _context.Categories.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<CategoryDto?> GetByIdAsync(string id)
    {
        var entity = await _context.Categories.FindAsync(id);
        return entity == null ? null : new CategoryDto { Id = entity.Id, Name = entity.Name, ParentId = entity.ParentId };
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var entities = await _context.Categories.ToListAsync();
        return entities.Select(e => new CategoryDto { Id = e.Id, Name = e.Name, ParentId = e.ParentId });
    }
}

public class ForWhomService : IForWhomService
{
    private readonly ApplicationDbContext _context;
    public ForWhomService(ApplicationDbContext context) => _context = context;

    public async Task<ForWhomDto> CreateAsync(ForWhomDto dto)
    {
        var entity = new ForWhom { Id = Guid.NewGuid().ToString(), ForWhomValue = dto.Name };
        _context.ForWhoms.Add(entity);
        await _context.SaveChangesAsync();
        return new ForWhomDto { Id = entity.Id, Name = entity.ForWhomValue };
    }

    public async Task<ForWhomDto> UpdateAsync(ForWhomDto dto)
    {
        var entity = await _context.ForWhoms.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"ForWhom with ID {dto.Id} not found");
        entity.ForWhomValue = dto.Name;
        await _context.SaveChangesAsync();
        return new ForWhomDto { Id = entity.Id, Name = entity.ForWhomValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.ForWhoms.FindAsync(id);
        if (entity != null) { _context.ForWhoms.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<ForWhomDto?> GetByIdAsync(string id)
    {
        var entity = await _context.ForWhoms.FindAsync(id);
        return entity == null ? null : new ForWhomDto { Id = entity.Id, Name = entity.ForWhomValue };
    }

    public async Task<IEnumerable<ForWhomDto>> GetAllAsync()
    {
        var entities = await _context.ForWhoms.ToListAsync();
        return entities.Select(e => new ForWhomDto { Id = e.Id, Name = e.ForWhomValue });
    }
}

public class ApplicationMethodService : IApplicationMethodService
{
    private readonly ApplicationDbContext _context;
    public ApplicationMethodService(ApplicationDbContext context) => _context = context;

    public async Task<ApplicationMethodDto> CreateAsync(ApplicationMethodDto dto)
    {
        var entity = new ApplicationMethod { Id = Guid.NewGuid().ToString(), ApplicationMethodValue = dto.Name };
        _context.ApplicationMethods.Add(entity);
        await _context.SaveChangesAsync();
        return new ApplicationMethodDto { Id = entity.Id, Name = entity.ApplicationMethodValue };
    }

    public async Task<ApplicationMethodDto> UpdateAsync(ApplicationMethodDto dto)
    {
        var entity = await _context.ApplicationMethods.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"ApplicationMethod with ID {dto.Id} not found");
        entity.ApplicationMethodValue = dto.Name;
        await _context.SaveChangesAsync();
        return new ApplicationMethodDto { Id = entity.Id, Name = entity.ApplicationMethodValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.ApplicationMethods.FindAsync(id);
        if (entity != null) { _context.ApplicationMethods.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<ApplicationMethodDto?> GetByIdAsync(string id)
    {
        var entity = await _context.ApplicationMethods.FindAsync(id);
        return entity == null ? null : new ApplicationMethodDto { Id = entity.Id, Name = entity.ApplicationMethodValue };
    }

    public async Task<IEnumerable<ApplicationMethodDto>> GetAllAsync()
    {
        var entities = await _context.ApplicationMethods.ToListAsync();
        return entities.Select(e => new ApplicationMethodDto { Id = e.Id, Name = e.ApplicationMethodValue });
    }
}

public class OrgansAndSystemService : IOrgansAndSystemService
{
    private readonly ApplicationDbContext _context;
    public OrgansAndSystemService(ApplicationDbContext context) => _context = context;

    public async Task<OrgansAndSystemDto> CreateAsync(OrgansAndSystemDto dto)
    {
        var entity = new OrgansAndSystem { Id = Guid.NewGuid().ToString(), OrgansAndSystemValue = dto.Name };
        _context.OrgansAndSystems.Add(entity);
        await _context.SaveChangesAsync();
        return new OrgansAndSystemDto { Id = entity.Id, Name = entity.OrgansAndSystemValue };
    }

    public async Task<OrgansAndSystemDto> UpdateAsync(OrgansAndSystemDto dto)
    {
        var entity = await _context.OrgansAndSystems.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"OrgansAndSystem with ID {dto.Id} not found");
        entity.OrgansAndSystemValue = dto.Name;
        await _context.SaveChangesAsync();
        return new OrgansAndSystemDto { Id = entity.Id, Name = entity.OrgansAndSystemValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.OrgansAndSystems.FindAsync(id);
        if (entity != null) { _context.OrgansAndSystems.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<OrgansAndSystemDto?> GetByIdAsync(string id)
    {
        var entity = await _context.OrgansAndSystems.FindAsync(id);
        return entity == null ? null : new OrgansAndSystemDto { Id = entity.Id, Name = entity.OrgansAndSystemValue };
    }

    public async Task<IEnumerable<OrgansAndSystemDto>> GetAllAsync()
    {
        var entities = await _context.OrgansAndSystems.ToListAsync();
        return entities.Select(e => new OrgansAndSystemDto { Id = e.Id, Name = e.OrgansAndSystemValue });
    }
}

public class PackagingMaterialService : IPackagingMaterialService
{
    private readonly ApplicationDbContext _context;
    public PackagingMaterialService(ApplicationDbContext context) => _context = context;

    public async Task<PackagingMaterialDto> CreateAsync(PackagingMaterialDto dto)
    {
        var entity = new PackagingMaterial { Id = Guid.NewGuid().ToString(), PackagingMaterialValue = dto.Name };
        _context.PackagingMaterials.Add(entity);
        await _context.SaveChangesAsync();
        return new PackagingMaterialDto { Id = entity.Id, Name = entity.PackagingMaterialValue };
    }

    public async Task<PackagingMaterialDto> UpdateAsync(PackagingMaterialDto dto)
    {
        var entity = await _context.PackagingMaterials.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PackagingMaterial with ID {dto.Id} not found");
        entity.PackagingMaterialValue = dto.Name;
        await _context.SaveChangesAsync();
        return new PackagingMaterialDto { Id = entity.Id, Name = entity.PackagingMaterialValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PackagingMaterials.FindAsync(id);
        if (entity != null) { _context.PackagingMaterials.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PackagingMaterialDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PackagingMaterials.FindAsync(id);
        return entity == null ? null : new PackagingMaterialDto { Id = entity.Id, Name = entity.PackagingMaterialValue };
    }

    public async Task<IEnumerable<PackagingMaterialDto>> GetAllAsync()
    {
        var entities = await _context.PackagingMaterials.ToListAsync();
        return entities.Select(e => new PackagingMaterialDto { Id = e.Id, Name = e.PackagingMaterialValue });
    }
}

public class PreparationColorService : IPreparationColorService
{
    private readonly ApplicationDbContext _context;
    public PreparationColorService(ApplicationDbContext context) => _context = context;

    public async Task<PreparationColorDto> CreateAsync(PreparationColorDto dto)
    {
        var entity = new PreparationColor { Id = Guid.NewGuid().ToString(), PreparationColorValue = dto.Name };
        _context.PreparationColors.Add(entity);
        await _context.SaveChangesAsync();
        return new PreparationColorDto { Id = entity.Id, Name = entity.PreparationColorValue };
    }

    public async Task<PreparationColorDto> UpdateAsync(PreparationColorDto dto)
    {
        var entity = await _context.PreparationColors.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PreparationColor with ID {dto.Id} not found");
        entity.PreparationColorValue = dto.Name;
        await _context.SaveChangesAsync();
        return new PreparationColorDto { Id = entity.Id, Name = entity.PreparationColorValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PreparationColors.FindAsync(id);
        if (entity != null) { _context.PreparationColors.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PreparationColorDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PreparationColors.FindAsync(id);
        return entity == null ? null : new PreparationColorDto { Id = entity.Id, Name = entity.PreparationColorValue };
    }

    public async Task<IEnumerable<PreparationColorDto>> GetAllAsync()
    {
        var entities = await _context.PreparationColors.ToListAsync();
        return entities.Select(e => new PreparationColorDto { Id = e.Id, Name = e.PreparationColorValue });
    }
}

public class PreparationMaterialService : IPreparationMaterialService
{
    private readonly ApplicationDbContext _context;
    public PreparationMaterialService(ApplicationDbContext context) => _context = context;

    public async Task<PreparationMaterialDto> CreateAsync(PreparationMaterialDto dto)
    {
        var entity = new PreparationMaterial { Id = Guid.NewGuid().ToString(), PreparationMaterialValue = dto.Name };
        _context.PreparationMaterials.Add(entity);
        await _context.SaveChangesAsync();
        return new PreparationMaterialDto { Id = entity.Id, Name = entity.PreparationMaterialValue };
    }

    public async Task<PreparationMaterialDto> UpdateAsync(PreparationMaterialDto dto)
    {
        var entity = await _context.PreparationMaterials.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"PreparationMaterial with ID {dto.Id} not found");
        entity.PreparationMaterialValue = dto.Name;
        await _context.SaveChangesAsync();
        return new PreparationMaterialDto { Id = entity.Id, Name = entity.PreparationMaterialValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.PreparationMaterials.FindAsync(id);
        if (entity != null) { _context.PreparationMaterials.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<PreparationMaterialDto?> GetByIdAsync(string id)
    {
        var entity = await _context.PreparationMaterials.FindAsync(id);
        return entity == null ? null : new PreparationMaterialDto { Id = entity.Id, Name = entity.PreparationMaterialValue };
    }

    public async Task<IEnumerable<PreparationMaterialDto>> GetAllAsync()
    {
        var entities = await _context.PreparationMaterials.ToListAsync();
        return entities.Select(e => new PreparationMaterialDto { Id = e.Id, Name = e.PreparationMaterialValue });
    }
}

public class ScopeOfApplicationService : IScopeOfApplicationService
{
    private readonly ApplicationDbContext _context;
    public ScopeOfApplicationService(ApplicationDbContext context) => _context = context;

    public async Task<ScopeOfApplicationDto> CreateAsync(ScopeOfApplicationDto dto)
    {
        var entity = new ScopeOfApplication { Id = Guid.NewGuid().ToString(), ScopeOfApplicationValue = dto.Name };
        _context.ScopeOfApplications.Add(entity);
        await _context.SaveChangesAsync();
        return new ScopeOfApplicationDto { Id = entity.Id, Name = entity.ScopeOfApplicationValue };
    }

    public async Task<ScopeOfApplicationDto> UpdateAsync(ScopeOfApplicationDto dto)
    {
        var entity = await _context.ScopeOfApplications.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"ScopeOfApplication with ID {dto.Id} not found");
        entity.ScopeOfApplicationValue = dto.Name;
        await _context.SaveChangesAsync();
        return new ScopeOfApplicationDto { Id = entity.Id, Name = entity.ScopeOfApplicationValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.ScopeOfApplications.FindAsync(id);
        if (entity != null) { _context.ScopeOfApplications.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<ScopeOfApplicationDto?> GetByIdAsync(string id)
    {
        var entity = await _context.ScopeOfApplications.FindAsync(id);
        return entity == null ? null : new ScopeOfApplicationDto { Id = entity.Id, Name = entity.ScopeOfApplicationValue };
    }

    public async Task<IEnumerable<ScopeOfApplicationDto>> GetAllAsync()
    {
        var entities = await _context.ScopeOfApplications.ToListAsync();
        return entities.Select(e => new ScopeOfApplicationDto { Id = e.Id, Name = e.ScopeOfApplicationValue });
    }
}

public class TimeOfApplicationService : ITimeOfApplicationService
{
    private readonly ApplicationDbContext _context;
    public TimeOfApplicationService(ApplicationDbContext context) => _context = context;

    public async Task<TimeOfApplicationDto> CreateAsync(TimeOfApplicationDto dto)
    {
        var entity = new TimeOfApplication { Id = Guid.NewGuid().ToString(), TimeOfApplicationValue = dto.Name };
        _context.TimeOfApplications.Add(entity);
        await _context.SaveChangesAsync();
        return new TimeOfApplicationDto { Id = entity.Id, Name = entity.TimeOfApplicationValue };
    }

    public async Task<TimeOfApplicationDto> UpdateAsync(TimeOfApplicationDto dto)
    {
        var entity = await _context.TimeOfApplications.FindAsync(dto.Id);
        if (entity == null) throw new KeyNotFoundException($"TimeOfApplication with ID {dto.Id} not found");
        entity.TimeOfApplicationValue = dto.Name;
        await _context.SaveChangesAsync();
        return new TimeOfApplicationDto { Id = entity.Id, Name = entity.TimeOfApplicationValue };
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await _context.TimeOfApplications.FindAsync(id);
        if (entity != null) { _context.TimeOfApplications.Remove(entity); await _context.SaveChangesAsync(); }
    }

    public async Task<TimeOfApplicationDto?> GetByIdAsync(string id)
    {
        var entity = await _context.TimeOfApplications.FindAsync(id);
        return entity == null ? null : new TimeOfApplicationDto { Id = entity.Id, Name = entity.TimeOfApplicationValue };
    }

    public async Task<IEnumerable<TimeOfApplicationDto>> GetAllAsync()
    {
        var entities = await _context.TimeOfApplications.ToListAsync();
        return entities.Select(e => new TimeOfApplicationDto { Id = e.Id, Name = e.TimeOfApplicationValue });
    }
}
