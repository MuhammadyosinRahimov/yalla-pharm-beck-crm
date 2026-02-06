using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IClientService
{
    Task<ClientDto> CreateAsync(ClientDto clientDto);
    Task<ClientDto> UpdateAsync(ClientDto clientDto);
    Task DeleteAsync(string id);
    Task<ClientDto?> GetByIdAsync(string id);
    Task<IEnumerable<ClientDto>> GetAllAsync();
}
