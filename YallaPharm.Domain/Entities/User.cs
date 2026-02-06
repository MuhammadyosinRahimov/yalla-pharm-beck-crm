using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

/// <summary>
/// Класс User представляет пользователя системы (Entity - сущность).
///
/// ОСНОВНЫЕ ПОНЯТИЯ C#:
///
/// 1. CLASS (Класс) - это шаблон для создания объектов.
///    Класс определяет свойства (данные) и методы (поведение) объекта.
///
/// 2. PUBLIC - модификатор доступа, означает что класс доступен везде в коде.
///    Другие модификаторы: private (только внутри класса), protected (класс и наследники).
///
/// 3. PROPERTY (Свойство) - это член класса, который предоставляет механизм
///    для чтения (get) и записи (set) значений.
///    Например: public string FirstName { get; set; }
///
/// 4. VIRTUAL - ключевое слово, позволяющее переопределить свойство в наследнике.
///    Используется для ленивой загрузки данных (Lazy Loading) в Entity Framework.
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    ///
    /// Guid.NewGuid().ToString() - создаёт новый уникальный идентификатор (GUID).
    /// GUID (Globally Unique Identifier) - 128-битное число, уникальное во всём мире.
    /// Пример: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Имя пользователя.
    ///
    /// string.Empty - пустая строка "" (лучше чем null для избежания NullReferenceException).
    /// Это значение по умолчанию, если имя не указано.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона пользователя.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хеш пароля пользователя.
    ///
    /// ВАЖНО: Никогда не храните пароли в открытом виде!
    /// Всегда используйте хеширование (например, BCrypt, SHA256).
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Роль пользователя в системе.
    ///
    /// UserRole - это enum (перечисление), которое определяет
    /// возможные роли: Admin, Operator, Courier и т.д.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Список заказов, которые доставляет этот пользователь (если он курьер).
    ///
    /// NAVIGATION PROPERTY (Навигационное свойство):
    /// - Позволяет перемещаться между связанными сущностями.
    /// - virtual - для ленивой загрузки (данные загружаются только при обращении).
    /// - ICollection&lt;T&gt; - коллекция, используется для связи "один ко многим".
    ///
    /// Пример использования:
    /// var courierOrders = user.CourierOrders; // Получить все заказы курьера
    /// </summary>
    public virtual ICollection<CourierOrder> CourierOrders { get; set; } = new List<CourierOrder>();

    /// <summary>
    /// Список заказов, которые обрабатывает этот пользователь (если он оператор).
    /// </summary>
    public virtual ICollection<OperatorOrder> OperatorOrders { get; set; } = new List<OperatorOrder>();
}
