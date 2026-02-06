using Microsoft.Extensions.DependencyInjection;
using YallaPharm.Application.Services;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.Application;

/// <summary>
/// Класс для регистрации сервисов Application слоя.
///
/// DEPENDENCY INJECTION (DI) - ВНЕДРЕНИЕ ЗАВИСИМОСТЕЙ:
///
/// DI - это паттерн, при котором объекты получают свои зависимости
/// извне, а не создают их сами.
///
/// ПРОБЛЕМА БЕЗ DI:
/// <code>
/// public class UserController
/// {
///     private readonly UserService _service = new UserService(); // Плохо!
///     // - Нельзя заменить на другую реализацию
///     // - Сложно тестировать
///     // - Жёсткая связь между классами
/// }
/// </code>
///
/// РЕШЕНИЕ С DI:
/// <code>
/// public class UserController
/// {
///     private readonly IUserService _service;
///
///     // Зависимость передаётся через конструктор
///     public UserController(IUserService service)
///     {
///         _service = service;
///     }
/// }
/// </code>
///
/// ВРЕМЕНА ЖИЗНИ СЕРВИСОВ (Service Lifetime):
///
/// 1. AddTransient - новый экземпляр при каждом запросе
///    Используйте для легковесных сервисов без состояния.
///
/// 2. AddScoped - один экземпляр на HTTP запрос
///    Идеально для работы с БД (DbContext).
///    Все сервисы в одном запросе используют один DbContext.
///
/// 3. AddSingleton - один экземпляр на всё приложение
///    Для конфигурации, кэширования. Осторожно с потокобезопасностью!
///
/// EXTENSION METHOD:
/// this IServiceCollection services - позволяет вызывать как:
/// services.AddApplication()
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует все сервисы Application слоя в DI контейнере.
    ///
    /// EXTENSION METHOD (Метод расширения):
    /// "this" перед первым параметром делает метод доступным
    /// как метод экземпляра IServiceCollection.
    ///
    /// Вызывается в Program.cs: builder.Services.AddApplication()
    ///
    /// ПАТТЕРН FLUENT INTERFACE:
    /// Возвращаем IServiceCollection для возможности цепочки вызовов:
    /// services.AddApplication().AddInfrastructure().AddSwagger();
    /// </summary>
    /// <param name="services">Коллекция сервисов ASP.NET Core</param>
    /// <returns>Та же коллекция для цепочки вызовов</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // =============================================================================
        // AddScoped - ОДИН ЭКЗЕМПЛЯР НА HTTP ЗАПРОС
        // =============================================================================
        //
        // Формат: AddScoped<IИнтерфейс, Реализация>()
        //
        // Когда контроллер запрашивает IUserService,
        // DI контейнер создаёт экземпляр UserService.
        //
        // Все классы в одном HTTP запросе получат ОДИН И ТОТ ЖЕ экземпляр.
        // Это важно для DbContext - все операции в одной транзакции.
        // =============================================================================

        // Сервис аутентификации (логин, JWT токены)
        services.AddScoped<IAuthService, AuthService>();

        // Сервис управления пользователями (CRUD)
        services.AddScoped<IUserService, UserService>();

        // Сервис работы с клиентами (покупателями)
        services.AddScoped<IClientService, ClientService>();

        // Сервис заказов (самый сложный - много бизнес-логики)
        services.AddScoped<IOrderService, OrderService>();

        // Сервис товаров
        services.AddScoped<IProductService, ProductService>();

        // Сервис аптек
        services.AddScoped<IPharmacyService, PharmacyService>();

        // =============================================================================
        // СПРАВОЧНИКИ (Reference Services)
        // =============================================================================

        // Типы упаковки
        services.AddScoped<IPackagingTypeService, PackagingTypeService>();

        // Единицы измерения
        services.AddScoped<IPackagingUnitService, PackagingUnitService>();

        // Способы оплаты
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();

        // Формы выпуска препаратов
        services.AddScoped<IReleaseFormService, ReleaseFormService>();

        // =============================================================================
        // СПРАВОЧНИКИ ШАБЛОНОВ ТОВАРОВ (Product Template Reference Services)
        // =============================================================================

        // Категории товаров
        services.AddScoped<ICategoryService, CategoryService>();

        // Для кого (возрастная группа)
        services.AddScoped<IForWhomService, ForWhomService>();

        // Способы применения
        services.AddScoped<IApplicationMethodService, ApplicationMethodService>();

        // Органы и системы
        services.AddScoped<IOrgansAndSystemService, OrgansAndSystemService>();

        // Материал упаковки
        services.AddScoped<IPackagingMaterialService, PackagingMaterialService>();

        // Цвет препарата
        services.AddScoped<IPreparationColorService, PreparationColorService>();

        // Материал препарата
        services.AddScoped<IPreparationMaterialService, PreparationMaterialService>();

        // Область применения
        services.AddScoped<IScopeOfApplicationService, ScopeOfApplicationService>();

        // Время применения
        services.AddScoped<ITimeOfApplicationService, TimeOfApplicationService>();

        // Шаблоны товаров (каталог)
        services.AddScoped<IProductTemplateService, ProductTemplateService>();

        // Сервис доставки
        services.AddScoped<IDeliveryService, DeliveryService>();

        // Интеграция с Telegram (уведомления)
        services.AddScoped<ITelegramService, TelegramService>();

        // Возвращаем коллекцию для возможности цепочки вызовов
        return services;
    }
}
