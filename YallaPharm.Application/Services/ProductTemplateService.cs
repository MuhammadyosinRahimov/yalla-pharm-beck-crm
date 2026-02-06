// ==============================================
// СЕРВИС ШАБЛОНОВ ТОВАРОВ (Product Template Service)
// ==============================================
// Управляет шаблонами товаров:
// - CRUD операции с шаблонами
// - Загрузка и хранение изображений
// ==============================================

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Exceptions;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

/// <summary>
/// Сервис для работы с шаблонами товаров.
/// Шаблон - это базовая информация о препарате, на основе которой
/// создаются конкретные товары в аптеках.
/// </summary>
public class ProductTemplateService : IProductTemplateService
{
    // ==== КОНСТАНТЫ ====
    /// <summary>
    /// Максимальный размер загружаемого изображения (5 МБ).
    /// </summary>
    private const long MaxImageSizeBytes = 5 * 1024 * 1024;

    /// <summary>
    /// Допустимые расширения файлов изображений.
    /// </summary>
    private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp"
    };

    /// <summary>
    /// Допустимые MIME-типы изображений.
    /// </summary>
    private static readonly HashSet<string> AllowedMimeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/gif", "image/webp"
    };

    // ==== ЗАВИСИМОСТИ ====
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductTemplateService> _logger;

    /// <summary>
    /// Конструктор с внедрением зависимостей.
    /// </summary>
    public ProductTemplateService(ApplicationDbContext context, ILogger<ProductTemplateService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Создаёт новый шаблон товара.
    /// </summary>
    /// <param name="dto">Данные шаблона</param>
    /// <returns>Созданный шаблон</returns>
    public async Task<ProductTemplateDto> CreateAsync(ProductTemplateDto dto)
    {
        // ==== ВАЛИДАЦИЯ ОБЯЗАТЕЛЬНЫХ ПОЛЕЙ ====
        ValidateRequiredFields(dto);

        var entity = new ProductTemplate
        {
            Id = Guid.NewGuid().ToString(),
            CategoryId = dto.CategoryId!,
            Name = dto.Name,
            Nickname = dto.Nickname,
            ActiveIngredient = dto.ActiveIngredient,
            Dosage = dto.Dosage,
            DosageUnit = dto.DosageUnit,
            MinimumQuantityPerPiece = dto.MinimumQuantityPerPiece,
            PackQuantityOrDrugVolume = dto.PackQuantityOrDrugVolume,
            PackagingUnit = dto.PackagingUnit,
            PreparationTaste = dto.PreparationTaste,
            AgeFrom = dto.AgeFrom,
            AgeTo = dto.AgeTo,
            Instructions = dto.Instructions,
            IndicationForUse = dto.IndicationForUse,
            ContraindicationForUse = dto.ContraindicationForUse,
            Symptom = dto.Symptom,
            ForAllergySufferers = dto.ForAllergySufferers,
            ForDiabetics = dto.ForDiabetics,
            ForPregnantWomen = dto.ForPregnantWomen,
            ForChildren = dto.ForChildren,
            ForDriver = dto.ForDriver,
            SeasonOfApplication = dto.SeasonOfApplication,
            DriedFruit = dto.DriedFruit,
            Comment = dto.Comment,
            WithCaution = dto.WithCaution,
            VacationCondition = dto.VacationCondition,
            ForWhomId = dto.ForWhomId!,
            ApplicationMethodId = dto.ApplicationMethodId!,
            OrgansAndSystemsId = dto.OrgansAndSystemsId,
            PackagingMaterialId = dto.PackagingMaterialId!,
            PreparationColorId = dto.PreparationColorId!,
            PreparationMaterialId = dto.PreparationMaterialId!,
            ScopeOfApplicationId = dto.ScopeOfApplicationId,
            TimeOfApplicationId = dto.TimeOfApplicationId!
        };

        _context.ProductTemplates.Add(entity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("ProductTemplate created: {Id} - {Name}", entity.Id, entity.Name);

        return MapToDto(entity);
    }

    /// <summary>
    /// Обновляет существующий шаблон товара.
    /// </summary>
    /// <param name="dto">Данные для обновления</param>
    /// <returns>Обновлённый шаблон</returns>
    public async Task<ProductTemplateDto> UpdateAsync(ProductTemplateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Id))
        {
            throw new ValidationException("Id is required for update");
        }

        var entity = await _context.ProductTemplates.FindAsync(dto.Id);

        if (entity == null)
        {
            _logger.LogWarning("ProductTemplate not found for update: {Id}", dto.Id);
            throw new NotFoundException("ProductTemplate", dto.Id);
        }

        // ==== ОБНОВЛЕНИЕ ПОЛЕЙ ====
        // Используем null-coalescing для сохранения текущих значений
        entity.CategoryId = dto.CategoryId ?? entity.CategoryId;
        entity.Name = dto.Name;
        entity.Nickname = dto.Nickname;
        entity.ActiveIngredient = dto.ActiveIngredient;
        entity.Dosage = dto.Dosage;
        entity.DosageUnit = dto.DosageUnit;
        entity.MinimumQuantityPerPiece = dto.MinimumQuantityPerPiece;
        entity.PackQuantityOrDrugVolume = dto.PackQuantityOrDrugVolume;
        entity.PackagingUnit = dto.PackagingUnit;
        entity.PreparationTaste = dto.PreparationTaste;
        entity.AgeFrom = dto.AgeFrom;
        entity.AgeTo = dto.AgeTo;
        entity.Instructions = dto.Instructions;
        entity.IndicationForUse = dto.IndicationForUse;
        entity.ContraindicationForUse = dto.ContraindicationForUse;
        entity.Symptom = dto.Symptom;
        entity.ForAllergySufferers = dto.ForAllergySufferers;
        entity.ForDiabetics = dto.ForDiabetics;
        entity.ForPregnantWomen = dto.ForPregnantWomen;
        entity.ForChildren = dto.ForChildren;
        entity.ForDriver = dto.ForDriver;
        entity.SeasonOfApplication = dto.SeasonOfApplication;
        entity.DriedFruit = dto.DriedFruit;
        entity.Comment = dto.Comment;
        entity.WithCaution = dto.WithCaution;
        entity.VacationCondition = dto.VacationCondition;
        entity.ForWhomId = dto.ForWhomId ?? entity.ForWhomId;
        entity.ApplicationMethodId = dto.ApplicationMethodId ?? entity.ApplicationMethodId;
        entity.OrgansAndSystemsId = dto.OrgansAndSystemsId;
        entity.PackagingMaterialId = dto.PackagingMaterialId ?? entity.PackagingMaterialId;
        entity.PreparationColorId = dto.PreparationColorId ?? entity.PreparationColorId;
        entity.PreparationMaterialId = dto.PreparationMaterialId ?? entity.PreparationMaterialId;
        entity.ScopeOfApplicationId = dto.ScopeOfApplicationId;
        entity.TimeOfApplicationId = dto.TimeOfApplicationId ?? entity.TimeOfApplicationId;

        await _context.SaveChangesAsync();

        _logger.LogInformation("ProductTemplate updated: {Id} - {Name}", entity.Id, entity.Name);

        return MapToDto(entity);
    }

    /// <summary>
    /// Удаляет шаблон товара по ID.
    /// </summary>
    public async Task DeleteAsync(string id)
    {
        var entity = await _context.ProductTemplates.FindAsync(id);

        if (entity != null)
        {
            _context.ProductTemplates.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("ProductTemplate deleted: {Id}", id);
        }
        else
        {
            _logger.LogWarning("ProductTemplate not found for deletion: {Id}", id);
        }
    }

    /// <summary>
    /// Получает шаблон товара по ID.
    /// </summary>
    public async Task<ProductTemplateDto?> GetByIdAsync(string id)
    {
        var entity = await _context.ProductTemplates.FindAsync(id);
        return entity == null ? null : MapToDto(entity);
    }

    /// <summary>
    /// Получает все шаблоны товаров.
    /// </summary>
    public async Task<IEnumerable<ProductTemplateDto>> GetAllAsync()
    {
        var entities = await _context.ProductTemplates.ToListAsync();
        return entities.Select(MapToDto);
    }

    /// <summary>
    /// Сохраняет изображение товара с валидацией и обработкой ошибок.
    /// </summary>
    /// <param name="dto">Данные изображения (ID шаблона + файл)</param>
    /// <returns>Относительный путь к сохранённому изображению</returns>
    public async Task<string> SaveImageAsync(ProductTemplateImageDto dto)
    {
        // ==== ВАЛИДАЦИЯ ВХОДНЫХ ДАННЫХ ====
        if (string.IsNullOrEmpty(dto.ProductTemplateId))
        {
            throw new ValidationException("ProductTemplateId is required");
        }

        if (dto.Image == null || dto.Image.Length == 0)
        {
            throw new ValidationException("Image file is required");
        }

        // ==== ВАЛИДАЦИЯ ФАЙЛА ====
        ValidateImageFile(dto.Image);

        // ==== ПОДГОТОВКА ДИРЕКТОРИИ ====
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

        try
        {
            // Создаём директорию если не существует
            Directory.CreateDirectory(uploadsFolder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create uploads directory: {Path}", uploadsFolder);
            throw new FileOperationException("CreateDirectory", uploadsFolder, ex);
        }

        // ==== ГЕНЕРАЦИЯ УНИКАЛЬНОГО ИМЕНИ ФАЙЛА ====
        var extension = Path.GetExtension(dto.Image.FileName).ToLowerInvariant();
        var fileName = $"{dto.ProductTemplateId}_{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        // ==== СОХРАНЕНИЕ ФАЙЛА ====
        try
        {
            // Асинхронное создание потока и копирование файла
            await using var stream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await dto.Image.CopyToAsync(stream);

            _logger.LogInformation("Image saved successfully: {FilePath} ({Size} bytes)",
                filePath, dto.Image.Length);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IO error while saving image: {FilePath}", filePath);
            throw new FileOperationException("SaveFile", fileName, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied while saving image: {FilePath}", filePath);
            throw new FileOperationException("SaveFile", fileName, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving image: {FilePath}", filePath);

            // Пытаемся удалить частично записанный файл
            TryDeleteFile(filePath);

            throw new FileOperationException("SaveFile", fileName, ex);
        }

        // ==== ВОЗВРАТ ОТНОСИТЕЛЬНОГО ПУТИ ====
        return $"/images/products/{fileName}";
    }

    // ==== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ====

    /// <summary>
    /// Валидирует обязательные поля при создании шаблона.
    /// </summary>
    private static void ValidateRequiredFields(ProductTemplateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrEmpty(dto.CategoryId))
            errors.Add("CategoryId", new[] { "CategoryId is required" });

        if (string.IsNullOrEmpty(dto.ForWhomId))
            errors.Add("ForWhomId", new[] { "ForWhomId is required" });

        if (string.IsNullOrEmpty(dto.ApplicationMethodId))
            errors.Add("ApplicationMethodId", new[] { "ApplicationMethodId is required" });

        if (string.IsNullOrEmpty(dto.PackagingMaterialId))
            errors.Add("PackagingMaterialId", new[] { "PackagingMaterialId is required" });

        if (string.IsNullOrEmpty(dto.PreparationColorId))
            errors.Add("PreparationColorId", new[] { "PreparationColorId is required" });

        if (string.IsNullOrEmpty(dto.PreparationMaterialId))
            errors.Add("PreparationMaterialId", new[] { "PreparationMaterialId is required" });

        if (string.IsNullOrEmpty(dto.TimeOfApplicationId))
            errors.Add("TimeOfApplicationId", new[] { "TimeOfApplicationId is required" });

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }
    }

    /// <summary>
    /// Валидирует загружаемый файл изображения.
    /// </summary>
    /// <param name="file">Загружаемый файл</param>
    private void ValidateImageFile(IFormFile file)
    {
        // Проверка размера файла
        if (file.Length > MaxImageSizeBytes)
        {
            var maxSizeMb = MaxImageSizeBytes / (1024 * 1024);
            _logger.LogWarning("Image file too large: {Size} bytes (max: {Max} MB)",
                file.Length, maxSizeMb);
            throw new ValidationException($"Image file is too large. Maximum size is {maxSizeMb} MB.");
        }

        // Проверка расширения файла
        var extension = Path.GetExtension(file.FileName);
        if (string.IsNullOrEmpty(extension) || !AllowedImageExtensions.Contains(extension))
        {
            _logger.LogWarning("Invalid image extension: {Extension}", extension);
            throw new ValidationException(
                $"Invalid file extension '{extension}'. Allowed extensions: {string.Join(", ", AllowedImageExtensions)}");
        }

        // Проверка MIME-типа
        if (!string.IsNullOrEmpty(file.ContentType) && !AllowedMimeTypes.Contains(file.ContentType))
        {
            _logger.LogWarning("Invalid image MIME type: {ContentType}", file.ContentType);
            throw new ValidationException(
                $"Invalid file type '{file.ContentType}'. Allowed types: {string.Join(", ", AllowedMimeTypes)}");
        }
    }

    /// <summary>
    /// Безопасно пытается удалить файл (без выброса исключений).
    /// </summary>
    private void TryDeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogDebug("Cleaned up partial file: {FilePath}", filePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete partial file: {FilePath}", filePath);
        }
    }

    /// <summary>
    /// Преобразует Entity в DTO.
    /// </summary>
    private static ProductTemplateDto MapToDto(ProductTemplate entity)
    {
        return new ProductTemplateDto
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            Name = entity.Name,
            Nickname = entity.Nickname,
            ActiveIngredient = entity.ActiveIngredient,
            Dosage = entity.Dosage,
            DosageUnit = entity.DosageUnit,
            MinimumQuantityPerPiece = entity.MinimumQuantityPerPiece,
            PackQuantityOrDrugVolume = entity.PackQuantityOrDrugVolume,
            PackagingUnit = entity.PackagingUnit,
            PreparationTaste = entity.PreparationTaste,
            AgeFrom = entity.AgeFrom,
            AgeTo = entity.AgeTo,
            Instructions = entity.Instructions,
            IndicationForUse = entity.IndicationForUse,
            ContraindicationForUse = entity.ContraindicationForUse,
            Symptom = entity.Symptom,
            ForAllergySufferers = entity.ForAllergySufferers,
            ForDiabetics = entity.ForDiabetics,
            ForPregnantWomen = entity.ForPregnantWomen,
            ForChildren = entity.ForChildren,
            ForDriver = entity.ForDriver,
            SeasonOfApplication = entity.SeasonOfApplication,
            DriedFruit = entity.DriedFruit,
            Comment = entity.Comment,
            WithCaution = entity.WithCaution,
            VacationCondition = entity.VacationCondition,
            ForWhomId = entity.ForWhomId,
            ApplicationMethodId = entity.ApplicationMethodId,
            OrgansAndSystemsId = entity.OrgansAndSystemsId,
            PackagingMaterialId = entity.PackagingMaterialId,
            PreparationColorId = entity.PreparationColorId,
            PreparationMaterialId = entity.PreparationMaterialId,
            ScopeOfApplicationId = entity.ScopeOfApplicationId,
            TimeOfApplicationId = entity.TimeOfApplicationId
        };
    }
}
