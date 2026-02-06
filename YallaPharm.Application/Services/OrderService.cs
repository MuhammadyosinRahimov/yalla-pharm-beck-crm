// ==============================================
// СЕРВИС ЗАКАЗОВ (Order Service)
// ==============================================
// Управляет жизненным циклом заказов:
// - Создание заказов
// - Изменение статусов
// - Получение списков по состоянию
// - Работа с историей заказов
// ==============================================

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Exceptions;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Domain.Enums;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

/// <summary>
/// Сервис для работы с заказами.
/// Реализует бизнес-логику управления заказами в CRM системе.
/// </summary>
public class OrderService : IOrderService
{
    // ==== ЗАВИСИМОСТИ ====
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderService> _logger;

    /// <summary>
    /// Конструктор с внедрением зависимостей.
    /// </summary>
    /// <param name="context">Контекст БД</param>
    /// <param name="logger">Логгер для записи событий</param>
    public OrderService(ApplicationDbContext context, ILogger<OrderService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Удаляет заказ по идентификатору.
    /// </summary>
    /// <param name="id">ID заказа</param>
    public async Task DeleteAsync(string id)
    {
        // FindAsync - быстрый поиск по первичному ключу
        var order = await _context.Orders.FindAsync(id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Order {OrderId} deleted", id);
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent order: {OrderId}", id);
        }
    }

    /// <summary>
    /// Обновляет состояние заказа.
    /// Центральный метод для всех переходов между статусами.
    /// </summary>
    /// <param name="dto">DTO с данными обновления</param>
    /// <returns>Обновлённый заказ или null если не найден</returns>
    public async Task<OrderDto?> UpdateStateAsync(UpdateOrderStateDto dto)
    {
        // ==== ВАЛИДАЦИЯ ====
        if (string.IsNullOrEmpty(dto.OrderId))
        {
            throw new ValidationException("OrderId is required");
        }

        // ==== ЗАГРУЗКА ЗАКАЗА С ИСТОРИЕЙ ====
        // Include загружает связанную сущность (eager loading)
        var order = await _context.Orders
            .Include(o => o.OrderHistory)
            .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

        if (order == null)
        {
            _logger.LogWarning("Order not found: {OrderId}", dto.OrderId);
            return null;
        }

        // ==== СОЗДАНИЕ ИСТОРИИ ЕСЛИ НЕ СУЩЕСТВУЕТ ====
        // Защита от null-reference
        if (order.OrderHistory == null)
        {
            order.OrderHistory = new OrderHistory
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = order.Id,
                CreatedAt = DateTime.UtcNow
            };
            _context.OrderHistories.Add(order.OrderHistory);
            _logger.LogInformation("Created OrderHistory for order: {OrderId}", order.Id);
        }

        // ==== СОХРАНЕНИЕ ПРЕДЫДУЩЕГО СОСТОЯНИЯ ====
        order.OrderHistory.PastState = order.OrderHistory.State;
        order.OrderHistory.State = dto.State;

        // ==== ОБРАБОТКА ПЕРЕХОДА В ЗАВИСИМОСТИ ОТ НОВОГО СОСТОЯНИЯ ====
        // switch expression - современный синтаксис C# для pattern matching
        switch (dto.State)
        {
            case OrderState.Application:
                // Консультация - начальное состояние
                order.OrderHistory.ConsultedAt = DateTime.UtcNow;
                break;

            case OrderState.InSearch:
                // В поиске - оператор ищет товары
                order.OrderHistory.SearchingAt = DateTime.UtcNow;
                order.OrderHistory.LongSearchReason = dto.LongSearchReason;
                break;

            case OrderState.WaitingClient:
                // Ожидание клиента - запланированный звонок
                order.OrderHistory.CallingAt = dto.CallingAt;
                break;

            case OrderState.Placement:
                // Размещение заказа в аптеках
                order.OrderHistory.PlacementAt = DateTime.UtcNow;
                break;

            case OrderState.Placed:
                // Заказ размещён
                order.OrderHistory.PlacedAt = DateTime.UtcNow;
                break;

            case OrderState.ReadyForShipment:
                // Готов к отправке
                order.OrderHistory.ReadyForShipmentAt = DateTime.UtcNow;
                break;

            case OrderState.AcceptedByCourier:
                // Принят курьером
                order.OrderHistory.CourierAcceptedAt = DateTime.UtcNow;
                order.Courier = dto.Courier;
                order.OrderHistory.CommentForCourier = dto.CommentForCourier;
                break;

            case OrderState.Received:
                // Курьер получил заказ
                order.OrderHistory.CourierReceivedAt = DateTime.UtcNow;
                break;

            case OrderState.OnTheWay:
                // В пути к клиенту
                order.OrderHistory.NotifiedAt = DateTime.UtcNow;
                break;

            case OrderState.Delivered:
                // Доставлен
                order.OrderHistory.DeliveredAt = DateTime.UtcNow;
                order.OrderHistory.DeliveredTime = DateTime.UtcNow;
                break;

            case OrderState.Canceled:
                // Отменён
                order.OrderHistory.CanceledAt = DateTime.UtcNow;
                order.OrderHistory.ReasonForOrderCancellation = dto.ReasonForOrderCancellation;
                break;

            case OrderState.Rejection:
                // Отклонён (временно)
                order.OrderHistory.WasRejection = true;
                order.OrderHistory.ReasonForOrderRejection = dto.ReasonForOrderRejection;
                // Сохраняем предыдущее состояние для возврата
                order.OrderHistory.PreviousStateBeforeRejectionState = (int?)order.OrderHistory.PastState;
                break;

            case OrderState.Returned:
                // Возврат товаров
                order.OrderHistory.IsReturned = true;
                order.OrderHistory.ReasonForReturningTheOrder = dto.ReasonForReturningTheOrder;
                break;
        }

        // ==== ОБНОВЛЕНИЕ ДОПОЛНИТЕЛЬНЫХ ПОЛЕЙ ====
        // Используем null-coalescing для безопасного присваивания

        if (dto.IndividualDeliveryTime.HasValue)
            order.OrderHistory.IndividualDeliveryTime = dto.IndividualDeliveryTime;

        if (!string.IsNullOrEmpty(dto.PaymentMethod))
            order.OrderHistory.PaymentMethod = dto.PaymentMethod;

        if (dto.PaymentStatus.HasValue)
            order.OrderHistory.PaymentStatus = dto.PaymentStatus;

        if (!string.IsNullOrEmpty(dto.Comment))
            order.OrderHistory.Message = dto.Comment;

        if (!string.IsNullOrEmpty(dto.ReasonForOrderDelay))
            order.OrderHistory.ReasonForOrderDelay = dto.ReasonForOrderDelay;

        // ==== СОХРАНЕНИЕ ИЗМЕНЕНИЙ ====
        await _context.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} state changed to {State}", order.Id, dto.State);

        return await GetByIdAsync(order.Id, null);
    }

    /// <summary>
    /// Получает номер заказа по ID.
    /// </summary>
    public async Task<string?> GetOrderNumberAsync(string id)
    {
        var order = await _context.Orders.FindAsync(id);
        return order?.OrderNumber;
    }

    /// <summary>
    /// Получает заказы по состоянию с пагинацией.
    /// </summary>
    /// <param name="state">Состояние заказа</param>
    /// <param name="count">Количество записей</param>
    /// <param name="orderOrClientInfo">Фильтр по номеру заказа или данным клиента</param>
    public async Task<IEnumerable<OrderDto>> GetByStateAsync(OrderState state, int count, string? orderOrClientInfo = null)
    {
        // ==== ПОСТРОЕНИЕ ЗАПРОСА ====
        // Include/ThenInclude - загрузка связанных данных
        var query = _context.Orders
            .Include(o => o.Client)
            .Include(o => o.OrderHistory)
            .Include(o => o.PharmacyOrders)
                .ThenInclude(po => po.Pharmacy)
            .Include(o => o.PharmacyOrders)
                .ThenInclude(po => po.ProductHistories)
                    .ThenInclude(ph => ph.Product)
            .Where(o => o.OrderHistory != null && o.OrderHistory.State == state);

        // ==== ФИЛЬТРАЦИЯ ПО ПОИСКУ ====
        if (!string.IsNullOrEmpty(orderOrClientInfo))
        {
            var searchTerm = orderOrClientInfo.ToLower();
            query = query.Where(o =>
                o.OrderNumber.ToLower().Contains(searchTerm) ||
                (o.Client != null && o.Client.FullName.ToLower().Contains(searchTerm)) ||
                (o.Client != null && o.Client.PhoneNumber.Contains(orderOrClientInfo)));
        }

        // ==== СОРТИРОВКА И ВЫПОЛНЕНИЕ ====
        // БЕЗОПАСНАЯ СОРТИРОВКА: проверяем OrderHistory на null
        var orders = await query
            .OrderByDescending(o => o.OrderHistory != null ? o.OrderHistory.CreatedAt : DateTime.MinValue)
            .Take(count)
            .ToListAsync();

        return orders.Select(MapToDto);
    }

    /// <summary>
    /// Получает заказы по фильтру состояния.
    /// </summary>
    public async Task<IEnumerable<OrderDto>> GetByStateFilterAsync(OrdersByStateFilterDto filter)
    {
        return await GetByStateAsync(filter.OrderState, filter.Count, filter.OrderOrClientInfo);
    }

    /// <summary>
    /// Получает заказ по ID с опциональной фильтрацией по состоянию.
    /// </summary>
    /// <param name="orderId">ID заказа</param>
    /// <param name="orderState">Опциональное состояние для фильтрации</param>
    public async Task<OrderDto?> GetByIdAsync(string orderId, OrderState? orderState = null)
    {
        var query = _context.Orders
            .Include(o => o.Client)
            .Include(o => o.OrderHistory)
            .Include(o => o.PharmacyOrders)
                .ThenInclude(po => po.Pharmacy)
            .Include(o => o.PharmacyOrders)
                .ThenInclude(po => po.ProductHistories)
                    .ThenInclude(ph => ph.Product)
            .Where(o => o.Id == orderId);

        if (orderState.HasValue)
        {
            query = query.Where(o => o.OrderHistory != null && o.OrderHistory.State == orderState);
        }

        var order = await query.FirstOrDefaultAsync();
        return order == null ? null : MapToDto(order);
    }

    /// <summary>
    /// Создаёт новый заказ в состоянии "Консультация".
    /// </summary>
    /// <param name="dto">Данные нового заказа</param>
    public async Task<OrderDto?> CreateConsultingAsync(OrderDto dto)
    {
        // ==== ВАЛИДАЦИЯ ====
        if (string.IsNullOrEmpty(dto.ClientId))
        {
            throw new ValidationException("ClientId is required");
        }

        // ==== СОЗДАНИЕ ЗАКАЗА ====
        var order = new Order
        {
            Id = Guid.NewGuid().ToString(),
            ClientId = dto.ClientId,
            OrderNumber = GenerateOrderNumber(),
            Comment = dto.Comment,
            CountryToDelivery = dto.CountryToDelivery,
            Courier = dto.Courier,
            CommentForCourier = dto.CommentForCourier,
            Operator = dto.Operator ?? "",
            TotalCost = dto.TotalCost,
            Prepayment = dto.Prepayment,
            RestPayment = dto.RestPayment,
            Discount = dto.Discount,
            TotalOrderAmountExcludingDelivery = dto.TotalOrderAmountExcludingDelivery,
            CityOrDistrict = dto.CityOrDistrict ?? "",
            PriceForDeliveryOutsideTheCity = dto.PriceForDeliveryOutsideTheCity,
            RemainingPayment = dto.RemainingPayment,
            AmountWithDiscount = dto.AmountWithDiscount,
            AmountWithMarkup = dto.AmountWithMarkup,
            AmountWithoutMarkup = dto.AmountWithoutMarkup,
            AmountWithDelivery = dto.AmountWithDelivery,
            AmountWithoutDelivery = dto.AmountWithoutDelivery,
            Type = dto.Type,
            ComesFrom = dto.ComesFrom,
            DeliveryType = dto.DeliveryType,
            DeliveredAt = dto.DeliveredAt
        };

        // ==== СОЗДАНИЕ ИСТОРИИ ЗАКАЗА ====
        var orderHistory = new OrderHistory
        {
            Id = Guid.NewGuid().ToString(),
            OrderId = order.Id,
            State = OrderState.Application,
            CreatedAt = DateTime.UtcNow,
            ConsultedAt = DateTime.UtcNow,
            LeadCreatedAt = DateTime.UtcNow
        };

        // ==== ДОБАВЛЕНИЕ ТОВАРОВ ИЗ АПТЕК ====
        if (dto.PharmacyOrders != null)
        {
            foreach (var poDto in dto.PharmacyOrders)
            {
                var pharmacyOrder = new PharmacyOrder
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order.Id,
                    PharmacyId = poDto.PharmacyId
                };

                if (poDto.ProductHistories != null)
                {
                    foreach (var phDto in poDto.ProductHistories)
                    {
                        pharmacyOrder.ProductHistories.Add(new ProductHistory
                        {
                            Id = Guid.NewGuid().ToString(),
                            ProductId = phDto.ProductId,
                            PharmacyOrderId = pharmacyOrder.Id,
                            Message = phDto.Message,
                            Count = phDto.Count,
                            AmountWithMarkup = phDto.AmountWithMarkup,
                            AmountWithoutMarkup = phDto.AmountWithoutMarkup,
                            CreatedAt = DateTime.UtcNow,
                            Comment = phDto.Comment
                        });
                    }
                }

                order.PharmacyOrders.Add(pharmacyOrder);
            }
        }

        // ==== СОХРАНЕНИЕ В БД ====
        _context.Orders.Add(order);
        _context.OrderHistories.Add(orderHistory);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Order {OrderNumber} created with ID {OrderId}", order.OrderNumber, order.Id);

        return await GetByIdAsync(order.Id, null);
    }

    // ==== МЕТОДЫ БЫСТРОГО ИЗМЕНЕНИЯ СОСТОЯНИЯ ====
    // Делегируют в UpdateStateAsync

    public async Task<OrderDto?> InSearchAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.InSearch });
    }

    public async Task<OrderDto?> WaitingClientAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.WaitingClient });
    }

    public async Task<OrderDto?> PlacementAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.Placement });
    }

    public async Task<OrderDto?> DeliveredAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.Delivered });
    }

    public async Task<OrderDto?> CancellationAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.Canceled });
    }

    public async Task<OrderDto?> RejectionAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.Rejection });
    }

    /// <summary>
    /// Возвращает заказ из состояния "Отклонён" в предыдущее состояние.
    /// </summary>
    public async Task<OrderDto?> ReturnFromRejectionAsync(string orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderHistory)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        // ==== БЕЗОПАСНАЯ ПРОВЕРКА НА NULL ====
        if (order?.OrderHistory == null)
        {
            _logger.LogWarning("Cannot return from rejection: order or history not found for {OrderId}", orderId);
            return null;
        }

        // Восстанавливаем предыдущее состояние
        var previousState = (OrderState)(order.OrderHistory.PreviousStateBeforeRejectionState ?? (int)OrderState.Application);
        order.OrderHistory.State = previousState;
        order.OrderHistory.WasRejection = false;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} returned from rejection to state {State}", orderId, previousState);

        return await GetByIdAsync(order.Id, null);
    }

    public async Task<OrderDto?> ReturnProductsAsync(string orderId)
    {
        return await UpdateStateAsync(new UpdateOrderStateDto { OrderId = orderId, State = OrderState.Returned });
    }

    /// <summary>
    /// Получает все заказы аптеки для конкретного заказа.
    /// </summary>
    public async Task<IEnumerable<PharmacyOrderDto>> GetPharmacyOrdersAsync(string orderId)
    {
        var pharmacyOrders = await _context.PharmacyOrders
            .Include(po => po.Pharmacy)
            .Include(po => po.ProductHistories)
                .ThenInclude(ph => ph.Product)
            .Where(po => po.OrderId == orderId)
            .ToListAsync();

        return pharmacyOrders.Select(MapPharmacyOrderToDto);
    }

    // ==== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ====

    /// <summary>
    /// Генерирует уникальный номер заказа.
    /// Формат: ORD-YYYYMMDD-XXXXXXXX
    /// </summary>
    private static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }

    /// <summary>
    /// Преобразует Entity в DTO.
    /// БЕЗОПАСНО ОБРАБАТЫВАЕТ NULL ЗНАЧЕНИЯ.
    /// </summary>
    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            ClientId = order.ClientId,
            OrderNumber = order.OrderNumber,
            Comment = order.Comment,
            CountryToDelivery = order.CountryToDelivery,
            Courier = order.Courier,
            CommentForCourier = order.CommentForCourier,
            Operator = order.Operator,
            TotalCost = order.TotalCost,
            Prepayment = order.Prepayment,
            RestPayment = order.RestPayment,
            Discount = order.Discount,
            TotalOrderAmountExcludingDelivery = order.TotalOrderAmountExcludingDelivery,
            CityOrDistrict = order.CityOrDistrict,
            PriceForDeliveryOutsideTheCity = order.PriceForDeliveryOutsideTheCity,
            RemainingPayment = order.RemainingPayment,
            AmountWithDiscount = order.AmountWithDiscount,
            AmountWithMarkup = order.AmountWithMarkup,
            AmountWithoutMarkup = order.AmountWithoutMarkup,
            AmountWithDelivery = order.AmountWithDelivery,
            AmountWithoutDelivery = order.AmountWithoutDelivery,
            Type = order.Type,
            ComesFrom = order.ComesFrom,
            DeliveryType = order.DeliveryType,
            DeliveredAt = order.DeliveredAt,

            // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ CLIENT ====
            Client = order.Client != null ? new ClientDto
            {
                Id = order.Client.Id,
                FullName = order.Client.FullName,
                PhoneNumber = order.Client.PhoneNumber,
                Street = order.Client.Street,
                Landmark = order.Client.Landmark
            } : null,

            // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ HISTORY ====
            OrderHistory = order.OrderHistory != null ? MapOrderHistoryToDto(order.OrderHistory) : null,

            // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ PHARMACY ORDERS ====
            PharmacyOrders = order.PharmacyOrders?.Select(MapPharmacyOrderToDto).ToList() ?? new List<PharmacyOrderDto>()
        };
    }

    private static OrderHistoryDto MapOrderHistoryToDto(OrderHistory oh)
    {
        return new OrderHistoryDto
        {
            Id = oh.Id,
            OrderId = oh.OrderId,
            Message = oh.Message,
            CreatedAt = oh.CreatedAt,
            IndividualDeliveryTime = oh.IndividualDeliveryTime,
            State = oh.State,
            PaymentStatus = oh.PaymentStatus,
            PaymentMethod = oh.PaymentMethod,
            IsReturned = oh.IsReturned,
            RequestDate = oh.RequestDate,
            OrderDate = oh.OrderDate,
            TimeForAcceptingRequest = oh.TimeForAcceptingRequest,
            TimeInformCustomerAboutProduct = oh.TimeInformCustomerAboutProduct,
            AmountOfTimeRespondClient = oh.AmountOfTimeRespondClient,
            TimeToObtainClientApproval = oh.TimeToObtainClientApproval,
            TimeToSendCheckToClient = oh.TimeToSendCheckToClient,
            RequestProcessingTime = oh.RequestProcessingTime,
            CommentForCourier = oh.CommentForCourier,
            ReasonForOrderDelay = oh.ReasonForOrderDelay,
            ReasonForOrderCancellation = oh.ReasonForOrderCancellation,
            ReasonForOrderRejection = oh.ReasonForOrderRejection,
            CallingAt = oh.CallingAt,
            LongSearchReason = oh.LongSearchReason,
            OrderProcessingTime = oh.OrderProcessingTime,
            AmountOfProcessingTime = oh.AmountOfProcessingTime,
            AmountOfDeliveryTime = oh.AmountOfDeliveryTime,
            DeliveredTime = oh.DeliveredTime,
            TimeOfCompletionOfInquiry = oh.TimeOfCompletionOfInquiry,
            ReasonForReturningTheOrder = oh.ReasonForReturningTheOrder,
            ReturnedProductsCount = oh.ReturnedProductsCount,
            WasRejection = oh.WasRejection,
            PastState = oh.PastState,
            DeliveredAt = oh.DeliveredAt,
            CourierAcceptedAt = oh.CourierAcceptedAt,
            NotifiedAt = oh.NotifiedAt,
            PlacedAt = oh.PlacedAt,
            ReadyForShipmentAt = oh.ReadyForShipmentAt,
            SearchingAt = oh.SearchingAt,
            LeadCreatedAt = oh.LeadCreatedAt,
            CourierReceivedAt = oh.CourierReceivedAt,
            ConsultedAt = oh.ConsultedAt,
            PlacementAt = oh.PlacementAt,
            CanceledAt = oh.CanceledAt,
            ConfirmedAt = oh.ConfirmedAt,
            PreviousStateBeforeRejectionState = oh.PreviousStateBeforeRejectionState,
            MinutesForEntireProcessFinish = oh.MinutesForEntireProcessFinish,
            MinutesForEntireProcessToPlacement = oh.MinutesForEntireProcessToPlacement
        };
    }

    private static PharmacyOrderDto MapPharmacyOrderToDto(PharmacyOrder po)
    {
        return new PharmacyOrderDto
        {
            Id = po.Id,
            OrderId = po.OrderId,
            PharmacyId = po.PharmacyId,

            // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ PHARMACY ====
            Pharmacy = po.Pharmacy != null ? new PharmacyDto
            {
                Id = po.Pharmacy.Id,
                Name = po.Pharmacy.Name,
                Address = po.Pharmacy.Address
            } : null,

            // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ PRODUCT HISTORIES ====
            ProductHistories = po.ProductHistories?.Select(ph => new ProductHistoryDto
            {
                Id = ph.Id,
                ProductId = ph.ProductId,
                PharmacyOrderId = ph.PharmacyOrderId,
                Message = ph.Message,
                LongSearchReason = ph.LongSearchReason,
                ReturnReason = ph.ReturnReason,
                IsReturned = ph.IsReturned,
                ReturnedCount = ph.ReturnedCount,
                Count = ph.Count,
                AmountWithMarkup = ph.AmountWithMarkup,
                AmountWithoutMarkup = ph.AmountWithoutMarkup,
                CreatedAt = ph.CreatedAt,
                ArrivalDate = ph.ArrivalDate,
                ReturnTo = ph.ReturnTo,
                Comment = ph.Comment,

                // ==== БЕЗОПАСНОЕ ПРЕОБРАЗОВАНИЕ PRODUCT ====
                Product = ph.Product != null ? new ProductDto
                {
                    Id = ph.Product.Id,
                    Name = ph.Product.Name,
                    PriceWithMarkup = ph.Product.PriceWithMarkup,
                    PriceWithoutMarkup = ph.Product.PriceWithoutMarkup,
                    PathImage = ph.Product.PathImage
                } : null
            }).ToList() ?? new List<ProductHistoryDto>()
        };
    }
}
