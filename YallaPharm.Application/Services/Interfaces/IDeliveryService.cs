using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

public interface IDeliveryService
{
    Task<bool> SendAsync(DeliverySendDto dto);
    Task<bool> UpdateAsync(DeliveryUpdateDto dto);
}
