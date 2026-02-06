using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IProductTemplateService
{
    Task<ProductTemplateDto> CreateAsync(ProductTemplateDto dto);
    Task<ProductTemplateDto> UpdateAsync(ProductTemplateDto dto);
    Task DeleteAsync(string id);
    Task<ProductTemplateDto?> GetByIdAsync(string id);
    Task<IEnumerable<ProductTemplateDto>> GetAllAsync();
    Task<string> SaveImageAsync(ProductTemplateImageDto dto);
}
