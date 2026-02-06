using Microsoft.EntityFrameworkCore;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data;

/// <summary>
/// Контекст базы данных приложения.
///
/// DbContext - ENTITY FRAMEWORK CORE:
///
/// DbContext - это "мост" между кодом C# и базой данных.
/// Он позволяет:
/// - Выполнять запросы к базе данных (SELECT, INSERT, UPDATE, DELETE)
/// - Отслеживать изменения объектов
/// - Сохранять изменения в базу данных
///
/// НАСЛЕДОВАНИЕ:
/// ApplicationDbContext : DbContext
/// - Наш класс наследует от DbContext все его возможности
/// - Мы добавляем свои DbSet для каждой таблицы
///
/// КАК РАБОТАЕТ:
/// 1. DbSet&lt;User&gt; Users представляет таблицу "Users" в базе данных
/// 2. Когда мы пишем _context.Users.ToList(), EF генерирует SQL: SELECT * FROM Users
/// 3. EF автоматически преобразует результат в объекты User
///
/// ПРИМЕР ИСПОЛЬЗОВАНИЯ:
/// <code>
/// // Получить всех пользователей
/// var users = await _context.Users.ToListAsync();
///
/// // Найти по условию
/// var admin = await _context.Users.FirstOrDefaultAsync(u => u.Role == UserRole.Admin);
///
/// // Добавить нового
/// _context.Users.Add(newUser);
/// await _context.SaveChangesAsync();
/// </code>
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Конструктор контекста базы данных.
    ///
    /// DbContextOptions - содержит настройки подключения к БД:
    /// - Connection string (строка подключения)
    /// - Тип базы данных (PostgreSQL, SQL Server и т.д.)
    ///
    /// base(options) - передаём настройки в базовый класс DbContext
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // =============================================================================
    // DbSet<T> - ТАБЛИЦЫ БАЗЫ ДАННЫХ
    // =============================================================================
    // Каждый DbSet представляет одну таблицу в базе данных.
    // Set<T>() - это метод DbContext, который возвращает DbSet для типа T.
    //
    // EXPRESSION-BODIED PROPERTY:
    // public DbSet<User> Users => Set<User>();
    // Это краткая форма записи свойства с getter.
    // Эквивалентно: public DbSet<User> Users { get { return Set<User>(); } }
    // =============================================================================

    /// <summary>
    /// Таблица пользователей системы (администраторы, операторы, курьеры).
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Таблица клиентов (покупателей).
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Таблица заказов.
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Таблица истории изменений заказов.
    /// </summary>
    public DbSet<OrderHistory> OrderHistories => Set<OrderHistory>();

    /// <summary>
    /// Таблица товаров в заказах.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Таблица аптек.
    /// </summary>
    public DbSet<Pharmacy> Pharmacies => Set<Pharmacy>();

    /// <summary>
    /// Таблица связи аптек и заказов.
    /// </summary>
    public DbSet<PharmacyOrder> PharmacyOrders => Set<PharmacyOrder>();

    /// <summary>
    /// Таблица истории изменений товаров.
    /// </summary>
    public DbSet<ProductHistory> ProductHistories => Set<ProductHistory>();

    /// <summary>
    /// Таблица адресов.
    /// </summary>
    public DbSet<Address> Addresses => Set<Address>();

    // =============================================================================
    // СПРАВОЧНИКИ (Reference Tables)
    // =============================================================================

    /// <summary>
    /// Формы выпуска препаратов (таблетки, капсулы, сироп и т.д.)
    /// </summary>
    public DbSet<ReleaseForm> ReleaseForms => Set<ReleaseForm>();

    /// <summary>
    /// Типы упаковки.
    /// </summary>
    public DbSet<PackagingType> PackagingTypes => Set<PackagingType>();

    /// <summary>
    /// Единицы измерения упаковки.
    /// </summary>
    public DbSet<PackagingUnit> PackagingUnits => Set<PackagingUnit>();

    /// <summary>
    /// Способы оплаты.
    /// </summary>
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

    /// <summary>
    /// Поставщики.
    /// </summary>
    public DbSet<Provider> Providers => Set<Provider>();

    /// <summary>
    /// Связь товаров и поставщиков.
    /// </summary>
    public DbSet<ProductProvider> ProductProviders => Set<ProductProvider>();

    /// <summary>
    /// Контакты аптек.
    /// </summary>
    public DbSet<PharmacyContact> PharmacyContacts => Set<PharmacyContact>();

    /// <summary>
    /// Дети клиентов.
    /// </summary>
    public DbSet<ClientChildren> ClientChildrens => Set<ClientChildren>();

    /// <summary>
    /// Взрослые члены семьи клиентов.
    /// </summary>
    public DbSet<ClientAdult> ClientAdults => Set<ClientAdult>();

    /// <summary>
    /// Заказы, назначенные курьерам.
    /// </summary>
    public DbSet<CourierOrder> CourierOrders => Set<CourierOrder>();

    /// <summary>
    /// Заказы, обрабатываемые операторами.
    /// </summary>
    public DbSet<OperatorOrder> OperatorOrders => Set<OperatorOrder>();

    /// <summary>
    /// Справочник стран.
    /// </summary>
    public DbSet<Country> Countries => Set<Country>();

    /// <summary>
    /// Категории товаров.
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>
    /// Для кого предназначен препарат.
    /// </summary>
    public DbSet<ForWhom> ForWhoms => Set<ForWhom>();

    /// <summary>
    /// Способы применения.
    /// </summary>
    public DbSet<ApplicationMethod> ApplicationMethods => Set<ApplicationMethod>();

    /// <summary>
    /// Органы и системы организма.
    /// </summary>
    public DbSet<OrgansAndSystem> OrgansAndSystems => Set<OrgansAndSystem>();

    /// <summary>
    /// Материалы упаковки.
    /// </summary>
    public DbSet<PackagingMaterial> PackagingMaterials => Set<PackagingMaterial>();

    /// <summary>
    /// Цвета препаратов.
    /// </summary>
    public DbSet<PreparationColor> PreparationColors => Set<PreparationColor>();

    /// <summary>
    /// Материалы препаратов.
    /// </summary>
    public DbSet<PreparationMaterial> PreparationMaterials => Set<PreparationMaterial>();

    /// <summary>
    /// Области применения.
    /// </summary>
    public DbSet<ScopeOfApplication> ScopeOfApplications => Set<ScopeOfApplication>();

    /// <summary>
    /// Время применения.
    /// </summary>
    public DbSet<TimeOfApplication> TimeOfApplications => Set<TimeOfApplication>();

    /// <summary>
    /// Шаблоны товаров (каталог препаратов).
    /// </summary>
    public DbSet<ProductTemplate> ProductTemplates => Set<ProductTemplate>();

    /// <summary>
    /// Настройка моделей и связей между таблицами.
    ///
    /// OnModelCreating - FLUENT API:
    ///
    /// Этот метод вызывается при создании модели базы данных.
    /// Здесь мы настраиваем:
    /// - Связи между таблицами (один-ко-многим, многие-ко-многим)
    /// - Первичные и внешние ключи
    /// - Ограничения (уникальность, обязательность)
    /// - Индексы для оптимизации запросов
    ///
    /// ApplyConfigurationsFromAssembly:
    /// Автоматически применяет все конфигурации из папки Configurations.
    /// Это чище, чем писать всё здесь - каждая сущность имеет свой файл конфигурации.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Вызываем базовую реализацию (важно!)
        base.OnModelCreating(modelBuilder);

        // Автоматически находит и применяет все IEntityTypeConfiguration<T>
        // из текущей сборки (Assembly)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
