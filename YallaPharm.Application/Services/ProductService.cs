using Microsoft.EntityFrameworkCore;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Type = dto.Type,
            Dosage = dto.Dosage,
            CountOnPackage = dto.CountOnPackage,
            AgeFrom = dto.AgeFrom,
            AgeTo = dto.AgeTo,
            PriceWithMarkup = dto.PriceWithMarkup,
            PriceWithoutMarkup = dto.PriceWithoutMarkup,
            Manufacturer = dto.Manufacturer,
            PathImage = dto.PathImage,
            Dificit = dto.Dificit,
            ReleaseForm = dto.ReleaseForm,
            PackagingUnit = dto.PackagingUnit,
            TypeOfPackaging = dto.TypeOfPackaging,
            LinkProduct = dto.LinkProduct,
            Country = dto.Country,
            AgeType = dto.AgeType,
            IsRequired = dto.IsRequired,
            State = dto.State
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto> UpdateAsync(ProductDto dto)
    {
        var product = await _context.Products.FindAsync(dto.Id);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {dto.Id} not found");

        product.Name = dto.Name;
        product.Type = dto.Type;
        product.Dosage = dto.Dosage;
        product.CountOnPackage = dto.CountOnPackage;
        product.AgeFrom = dto.AgeFrom;
        product.AgeTo = dto.AgeTo;
        product.PriceWithMarkup = dto.PriceWithMarkup;
        product.PriceWithoutMarkup = dto.PriceWithoutMarkup;
        product.Manufacturer = dto.Manufacturer;
        product.PathImage = dto.PathImage;
        product.Dificit = dto.Dificit;
        product.ReleaseForm = dto.ReleaseForm;
        product.PackagingUnit = dto.PackagingUnit;
        product.TypeOfPackaging = dto.TypeOfPackaging;
        product.LinkProduct = dto.LinkProduct;
        product.Country = dto.Country;
        product.AgeType = dto.AgeType;
        product.IsRequired = dto.IsRequired;
        product.State = dto.State;

        await _context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task DeleteAsync(string id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ProductDto?> GetByIdAsync(string id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products.Select(MapToDto);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Type = product.Type,
            Dosage = product.Dosage,
            CountOnPackage = product.CountOnPackage,
            AgeFrom = product.AgeFrom,
            AgeTo = product.AgeTo,
            PriceWithMarkup = product.PriceWithMarkup,
            PriceWithoutMarkup = product.PriceWithoutMarkup,
            Manufacturer = product.Manufacturer,
            PathImage = product.PathImage,
            Dificit = product.Dificit,
            ReleaseForm = product.ReleaseForm,
            PackagingUnit = product.PackagingUnit,
            TypeOfPackaging = product.TypeOfPackaging,
            LinkProduct = product.LinkProduct,
            Country = product.Country,
            AgeType = product.AgeType,
            IsRequired = product.IsRequired,
            State = product.State
        };
    }
}
