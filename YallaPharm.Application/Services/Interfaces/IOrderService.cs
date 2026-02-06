using YallaPharm.Application.DTOs;
using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.Services.Interfaces;

public interface IOrderService
{
    Task DeleteAsync(string id);
    Task<OrderDto?> UpdateStateAsync(UpdateOrderStateDto dto);
    Task<string?> GetOrderNumberAsync(string id);
    Task<IEnumerable<OrderDto>> GetByStateAsync(OrderState state, int count, string? orderOrClientInfo = null);
    Task<IEnumerable<OrderDto>> GetByStateFilterAsync(OrdersByStateFilterDto filter);
    Task<OrderDto?> GetByIdAsync(string orderId, OrderState? orderState = null);
    Task<OrderDto?> CreateConsultingAsync(OrderDto orderDto);
    Task<OrderDto?> InSearchAsync(string orderId);
    Task<OrderDto?> WaitingClientAsync(string orderId);
    Task<OrderDto?> PlacementAsync(string orderId);
    Task<OrderDto?> DeliveredAsync(string orderId);
    Task<OrderDto?> CancellationAsync(string orderId);
    Task<OrderDto?> RejectionAsync(string orderId);
    Task<OrderDto?> ReturnFromRejectionAsync(string orderId);
    Task<OrderDto?> ReturnProductsAsync(string orderId);
    Task<IEnumerable<PharmacyOrderDto>> GetPharmacyOrdersAsync(string orderId);
}
