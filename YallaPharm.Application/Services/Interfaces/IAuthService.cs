using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
}
