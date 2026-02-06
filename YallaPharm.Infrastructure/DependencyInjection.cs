// ==============================================
// РЕГИСТРАЦИЯ ЗАВИСИМОСТЕЙ ИНФРАСТРУКТУРЫ (Infrastructure DI)
// ==============================================
// Этот файл настраивает все сервисы слоя инфраструктуры:
// - Подключение к базе данных PostgreSQL
// - Настройка Entity Framework Core
// - Регистрация репозиториев
// ==============================================

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YallaPharm.Infrastructure.Data;
using YallaPharm.Infrastructure.Repositories;

namespace YallaPharm.Infrastructure;

/// <summary>
/// Статический класс с методами расширения для регистрации сервисов.
/// Паттерн Extension Method позволяет "расширять" существующие классы.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует все сервисы инфраструктурного слоя в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации</param>
    /// <param name="configuration">Конфигурация приложения (appsettings.json)</param>
    /// <returns>Коллекция сервисов для цепочки вызовов (fluent pattern)</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ==== ПОЛУЧЕНИЕ СТРОКИ ПОДКЛЮЧЕНИЯ ====
        // Строка подключения содержит адрес БД, логин, пароль и т.д.
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' is not configured. " +
                "Please add it to appsettings.json in the ConnectionStrings section.");
        }

        // ==== НАСТРОЙКА ENTITY FRAMEWORK CORE ====
        // AddDbContext регистрирует DbContext с временем жизни Scoped (один экземпляр на запрос)
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // UseNpgsql - подключаем провайдер PostgreSQL
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                // ==== RETRY POLICY (ПОЛИТИКА ПОВТОРНЫХ ПОПЫТОК) ====
                // При временных сбоях БД (сеть, перезагрузка) EF Core повторит запрос
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,                           // Максимум 5 попыток
                    maxRetryDelay: TimeSpan.FromSeconds(30),    // Максимальная задержка между попытками
                    errorCodesToAdd: null);                     // Использовать стандартные коды ошибок PostgreSQL

                // ==== TIMEOUT КОМАНД ====
                // Максимальное время ожидания выполнения SQL-запроса
                npgsqlOptions.CommandTimeout(60);               // 60 секунд

                // ==== МИГРАЦИИ ====
                // Указываем сборку, где искать миграции
                npgsqlOptions.MigrationsAssembly("YallaPharm.Infrastructure");
            })
            // ==== LAZY LOADING ОТКЛЮЧЕН ====
            // Прокси для ленивой загрузки отключены, используем явную загрузку через .Include()
            // Причина: lazy loading вызывал N+1 запросы и ошибки при сериализации
            // .UseLazyLoadingProxies();
            ;

            // ==== ЛОГИРОВАНИЕ SQL (только для Development) ====
            #if DEBUG
            options.EnableSensitiveDataLogging();   // Показывать параметры запросов в логах
            options.EnableDetailedErrors();          // Детальные сообщения об ошибках
            #endif
        });

        // ==== ПРОВЕРКА ПОДКЛЮЧЕНИЯ К БД ====
        // Добавляем Health Check для мониторинга состояния БД
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(
                name: "database",
                tags: new[] { "db", "sql", "postgresql" });

        // ==== РЕГИСТРАЦИЯ GENERIC REPOSITORY ====
        // typeof(IRepository<>) - открытый generic тип (без указания конкретного T)
        // EF Core сам создаст нужный Repository<T> при запросе IRepository<User>, IRepository<Order> и т.д.
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
