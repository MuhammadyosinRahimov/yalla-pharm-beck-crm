using YallaPharm.Application.DTOs;

namespace YallaPharm.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса пользователей.
///
/// INTERFACE (Интерфейс) - ОСНОВЫ C#:
///
/// Интерфейс - это контракт, который определяет ЧТО должен делать класс,
/// но НЕ определяет КАК он это делает.
///
/// ЗАЧЕМ НУЖЕН ИНТЕРФЕЙС:
/// 1. Абстракция - скрывает реализацию, показывает только методы.
/// 2. Тестирование - легко создать mock-объект для тестов.
/// 3. Dependency Injection - можно легко заменить реализацию.
/// 4. Loose Coupling - уменьшает связанность кода.
///
/// NAMING CONVENTION (Соглашение об именовании):
/// Интерфейсы начинаются с буквы "I": IUserService, IRepository и т.д.
///
/// ПРИМЕР ИСПОЛЬЗОВАНИЯ:
/// <code>
/// // В контроллере используем интерфейс, не конкретный класс
/// public class UserController
/// {
///     private readonly IUserService _userService;
///
///     // Dependency Injection: ASP.NET автоматически передаёт реализацию
///     public UserController(IUserService userService)
///     {
///         _userService = userService;
///     }
/// }
/// </code>
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Создаёт нового пользователя.
    ///
    /// ASYNC/AWAIT (Асинхронность):
    /// - Task&lt;T&gt; - обещание вернуть значение типа T в будущем.
    /// - async/await - позволяет выполнять операции без блокировки потока.
    ///
    /// ПОЧЕМУ АСИНХРОННОСТЬ ВАЖНА:
    /// Пока идёт запрос к базе данных, поток может обрабатывать другие запросы.
    /// Это особенно важно для веб-серверов с большой нагрузкой.
    ///
    /// ПРИМЕР:
    /// <code>
    /// // await "ждёт" завершения операции, но не блокирует поток
    /// var user = await _userService.CreateAsync(userDto);
    /// </code>
    /// </summary>
    /// <param name="userDto">Данные нового пользователя</param>
    /// <returns>Созданный пользователь</returns>
    Task<UserResponseDto> CreateAsync(UserDto userDto);

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    /// <param name="userDto">Обновлённые данные пользователя (должен содержать Id)</param>
    /// <returns>Обновлённый пользователь</returns>
    Task<UserResponseDto> UpdateAsync(UserDto userDto);

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    ///
    /// Task (без &lt;T&gt;) - асинхронная операция без возвращаемого значения.
    /// Эквивалент void для асинхронных методов.
    /// </summary>
    /// <param name="id">Идентификатор пользователя для удаления</param>
    Task DeleteAsync(string id);

    /// <summary>
    /// Получает пользователя по идентификатору.
    ///
    /// NULLABLE RETURN TYPE:
    /// UserResponseDto? - метод может вернуть null, если пользователь не найден.
    /// Это явно показывает, что результат может отсутствовать.
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Пользователь или null, если не найден</returns>
    Task<UserResponseDto?> GetByIdAsync(string id);

    /// <summary>
    /// Получает список всех пользователей.
    ///
    /// IEnumerable&lt;T&gt; - интерфейс для перебора коллекции.
    /// Позволяет использовать foreach и LINQ.
    ///
    /// ПРИМЕР:
    /// <code>
    /// var users = await _userService.GetAllAsync();
    /// foreach (var user in users)
    /// {
    ///     Console.WriteLine(user.FirstName);
    /// }
    /// </code>
    /// </summary>
    /// <returns>Список всех пользователей</returns>
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
}
