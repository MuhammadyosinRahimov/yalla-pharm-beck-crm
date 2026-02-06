using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface ITelegramService
{
    Task ProcessUpdateAsync(TelegramUpdateDto update);
    Task<TelegramStatusDto> GetStatusAsync();
}
