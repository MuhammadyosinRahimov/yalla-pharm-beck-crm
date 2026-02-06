using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IPharmacyService
{
    Task<PharmacyDto> CreateAsync(PharmacyDto pharmacyDto);
    Task<PharmacyDto> UpdateAsync(PharmacyDto pharmacyDto);
    Task DeleteAsync(string id);
    Task<PharmacyDto?> GetByIdAsync(string id);
    Task<IEnumerable<PharmacyDto>> GetAllAsync();
}
