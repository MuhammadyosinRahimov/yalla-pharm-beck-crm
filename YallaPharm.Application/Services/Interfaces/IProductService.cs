
using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(ProductDto productDto);
    Task<ProductDto> UpdateAsync(ProductDto productDto);
    Task DeleteAsync(string id);
    Task<ProductDto?> GetByIdAsync(string id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
}
