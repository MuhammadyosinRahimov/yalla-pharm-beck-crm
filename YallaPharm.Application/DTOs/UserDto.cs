using YallaPharm.Domain.Enums;

namespace YallaPharm.Application.DTOs;

/// <summary>
/// DTO (Data Transfer Object) для передачи данных пользователя.
///
/// DTO - ПАТТЕРН ПРОЕКТИРОВАНИЯ:
///
/// DTO - это объект для передачи данных между слоями приложения.
///
/// ЗАЧЕМ НУЖЕН DTO:
/// 1. Скрывает внутреннюю структуру Entity от внешнего мира.
/// 2. Позволяет передавать только нужные данные (не все поля Entity).
/// 3. Защищает от случайного изменения данных в базе.
/// 4. Упрощает валидацию входных данных.
///
/// ПРИМЕР ИСПОЛЬЗОВАНИЯ:
/// - При создании пользователя: клиент отправляет UserDto
/// - Мы создаём User (Entity) из UserDto
/// - Сохраняем User в базу данных
///
/// ВАЖНО: Entity (User) - это модель базы данных.
/// DTO (UserDto) - это модель для API/клиента.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Идентификатор пользователя.
    ///
    /// NULLABLE TYPE (Nullable тип):
    /// string? - означает что значение может быть null.
    /// Используется при создании нового пользователя, когда Id ещё не задан.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль пользователя (только при создании/обновлении).
    ///
    /// string? - пароль может быть null при обновлении,
    /// если пользователь не хочет менять пароль.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public UserRole Role { get; set; }
}

/// <summary>
/// DTO для ответа API с данными пользователя.
///
/// ПОЧЕМУ ОТДЕЛЬНЫЙ DTO ДЛЯ ОТВЕТА:
/// - Не содержит пароль (безопасность!)
/// - Role преобразован в string для удобства отображения
/// - Все поля обязательные (без null)
/// </summary>
public class UserResponseDto
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Роль пользователя в текстовом виде ("Admin", "Operator" и т.д.)
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
