// ==============================================
// СЕРВИС ДОСТАВКИ (Delivery Service)
// ==============================================
// Управляет процессом доставки заказов:
// - Назначение курьера
// - Обновление статуса доставки
// ==============================================

using Microsoft.Extensions.Logging;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Exceptions;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.Services;

/// <summary>
/// Сервис для управления доставкой заказов.
/// Работает поверх OrderService для изменения состояний, связанных с доставкой.
/// </summary>
public class DeliveryService : IDeliveryService
{
    // ==== ЗАВИСИМОСТИ ====
    private readonly IOrderService _orderService;
    private readonly ILogger<DeliveryService> _logger;

    /// <summary>
    /// Допустимые значения статусов доставки (для валидации).
    /// </summary>
    private static readonly Dictionary<string, OrderState> _validDeliveryStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        { "received", OrderState.Received },
        { "ontheway", OrderState.OnTheWay },
        { "on_the_way", OrderState.OnTheWay },
        { "delivered", OrderState.Delivered }
    };

    /// <summary>
    /// Конструктор с внедрением зависимостей.
    /// </summary>
    /// <param name="orderService">Сервис заказов</param>
    /// <param name="logger">Логгер для записи событий</param>
    public DeliveryService(IOrderService orderService, ILogger<DeliveryService> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    /// <summary>
    /// Назначает курьера на заказ.
    /// Переводит заказ в состояние "Принят курьером".
    /// </summary>
    /// <param name="dto">Данные для отправки (ID заказа, ID курьера, комментарий)</param>
    /// <returns>true если успешно назначен</returns>
    public async Task<bool> SendAsync(DeliverySendDto dto)
    {
        // ==== ВАЛИДАЦИЯ ВХОДНЫХ ДАННЫХ ====
        if (string.IsNullOrWhiteSpace(dto.OrderId))
        {
            _logger.LogWarning("SendAsync called with empty OrderId");
            throw new ValidationException("OrderId is required");
        }

        if (string.IsNullOrWhiteSpace(dto.CourierId))
        {
            _logger.LogWarning("SendAsync called with empty CourierId for order {OrderId}", dto.OrderId);
            throw new ValidationException("CourierId is required");
        }

        _logger.LogInformation("Assigning courier {CourierId} to order {OrderId}", dto.CourierId, dto.OrderId);

        // ==== ОБНОВЛЕНИЕ СОСТОЯНИЯ ЗАКАЗА ====
        var result = await _orderService.UpdateStateAsync(new UpdateOrderStateDto
        {
            OrderId = dto.OrderId,
            State = OrderState.AcceptedByCourier,
            Courier = dto.CourierId,
            Comment = dto.Comment
        });

        if (result != null)
        {
            _logger.LogInformation("Courier {CourierId} successfully assigned to order {OrderId}",
                dto.CourierId, dto.OrderId);
            return true;
        }

        _logger.LogWarning("Failed to assign courier to order {OrderId} - order may not exist", dto.OrderId);
        return false;
    }

    /// <summary>
    /// Обновляет статус доставки.
    /// Принимает строковое значение статуса и преобразует в OrderState.
    /// </summary>
    /// <param name="dto">Данные обновления (ID заказа, статус, комментарий)</param>
    /// <returns>true если статус успешно обновлён</returns>
    public async Task<bool> UpdateAsync(DeliveryUpdateDto dto)
    {
        // ==== ВАЛИДАЦИЯ ВХОДНЫХ ДАННЫХ ====
        if (string.IsNullOrWhiteSpace(dto.OrderId))
        {
            _logger.LogWarning("UpdateAsync called with empty OrderId");
            throw new ValidationException("OrderId is required");
        }

        if (string.IsNullOrWhiteSpace(dto.Status))
        {
            _logger.LogWarning("UpdateAsync called with empty Status for order {OrderId}", dto.OrderId);
            throw new ValidationException("Status is required");
        }

        // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ СТРОКИ В ENUM ====
        // Используем словарь для поддержки разных форматов
        if (!TryParseDeliveryStatus(dto.Status, out var state))
        {
            _logger.LogWarning("Invalid delivery status '{Status}' for order {OrderId}. " +
                               "Valid values: {ValidStatuses}",
                dto.Status, dto.OrderId, string.Join(", ", _validDeliveryStatuses.Keys));

            throw new ValidationException(
                $"Invalid delivery status '{dto.Status}'. " +
                $"Valid values: {string.Join(", ", _validDeliveryStatuses.Keys)}");
        }

        _logger.LogInformation("Updating delivery status to {Status} for order {OrderId}",
            state, dto.OrderId);

        // ==== ОБНОВЛЕНИЕ СОСТОЯНИЯ ЗАКАЗА ====
        var result = await _orderService.UpdateStateAsync(new UpdateOrderStateDto
        {
            OrderId = dto.OrderId,
            State = state,
            Comment = dto.Comment
        });

        if (result != null)
        {
            _logger.LogInformation("Delivery status updated to {Status} for order {OrderId}",
                state, dto.OrderId);
            return true;
        }

        _logger.LogWarning("Failed to update delivery status for order {OrderId} - order may not exist",
            dto.OrderId);
        return false;
    }

    /// <summary>
    /// Пытается преобразовать строку в OrderState для доставки.
    /// Поддерживает различные форматы написания (camelCase, snake_case).
    /// </summary>
    /// <param name="status">Строковое значение статуса</param>
    /// <param name="state">Результат преобразования</param>
    /// <returns>true если преобразование успешно</returns>
    private static bool TryParseDeliveryStatus(string status, out OrderState state)
    {
        // Нормализуем строку: убираем пробелы, приводим к нижнему регистру
        var normalizedStatus = status.Trim().ToLowerInvariant();

        // Пробуем найти в словаре допустимых значений
        if (_validDeliveryStatuses.TryGetValue(normalizedStatus, out state))
        {
            return true;
        }

        // Пробуем стандартный парсинг enum (на случай если передали точное имя)
        if (Enum.TryParse<OrderState>(status, ignoreCase: true, out state))
        {
            // Проверяем, что это допустимый статус для доставки
            if (state == OrderState.Received ||
                state == OrderState.OnTheWay ||
                state == OrderState.Delivered)
            {
                return true;
            }
        }

        state = OrderState.Undefined;
        return false;
    }

    /// <summary>
    /// Получает список допустимых статусов доставки.
    /// Полезно для валидации на клиенте или документации API.
    /// </summary>
    /// <returns>Список допустимых строковых значений статусов</returns>
    public static IEnumerable<string> GetValidDeliveryStatuses()
    {
        return _validDeliveryStatuses.Keys;
    }
}
