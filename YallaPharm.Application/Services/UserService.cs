using Microsoft.EntityFrameworkCore;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

/// <summary>
/// Сервис для работы с пользователями.
///
/// SERVICE (Сервис) - БИЗНЕС-ЛОГИКА:
///
/// Сервис содержит бизнес-логику приложения:
/// - Создание, обновление, удаление пользователей
/// - Валидация данных
/// - Преобразование между Entity и DTO
///
/// DEPENDENCY INJECTION (Внедрение зависимостей):
/// Сервис получает ApplicationDbContext через конструктор.
/// ASP.NET Core автоматически передаёт нужный экземпляр.
///
/// ПАТТЕРН SERVICE LAYER:
/// Сервис находится между Controller и Repository/DbContext.
/// Controller -> Service -> DbContext -> База данных
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// Контекст базы данных для работы с пользователями.
    ///
    /// PRIVATE READONLY:
    /// - private - доступен только внутри этого класса
    /// - readonly - можно установить только в конструкторе, нельзя изменить позже
    ///
    /// NAMING CONVENTION:
    /// Приватные поля начинаются с "_" (underscore): _context, _userService
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Конструктор сервиса.
    ///
    /// CONSTRUCTOR (Конструктор):
    /// Метод, который вызывается при создании экземпляра класса.
    /// Используется для инициализации полей и внедрения зависимостей.
    ///
    /// DEPENDENCY INJECTION:
    /// ASP.NET Core автоматически создаёт и передаёт ApplicationDbContext.
    /// Это настраивается в Program.cs через builder.Services.AddDbContext().
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Создаёт нового пользователя в базе данных.
    ///
    /// ASYNC/AWAIT:
    /// - async - метод асинхронный, может использовать await
    /// - await - "ждём" завершения асинхронной операции
    /// - Task&lt;T&gt; - результат будет возвращён в будущем
    /// </summary>
    /// <param name="userDto">Данные нового пользователя</param>
    /// <returns>Созданный пользователь</returns>
    public async Task<UserResponseDto> CreateAsync(UserDto userDto)
    {
        // OBJECT INITIALIZER (Инициализатор объекта):
        // Создаём объект и сразу задаём значения свойств
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),      // Генерируем новый уникальный ID
            FirstName = userDto.FirstName,        // Копируем данные из DTO
            LastName = userDto.LastName,
            PhoneNumber = userDto.PhoneNumber,
            Email = userDto.Email,
            Password = AuthService.HashPassword(userDto.Password ?? ""),  // Хешируем пароль!
            Role = userDto.Role
        };

        // DbSet.Add() - добавляет сущность в контекст (ещё не в базу!)
        _context.Users.Add(user);

        // SaveChangesAsync() - сохраняет ВСЕ изменения в базу данных
        // Это единственный момент, когда данные реально записываются в БД
        await _context.SaveChangesAsync();

        // Возвращаем DTO (не Entity!) для безопасности
        return MapToResponseDto(user);
    }

    /// <summary>
    /// Обновляет данные существующего пользователя.
    /// </summary>
    public async Task<UserResponseDto> UpdateAsync(UserDto userDto)
    {
        // FindAsync() - ищет сущность по первичному ключу (Id)
        // Это самый эффективный способ поиска по Id
        var user = await _context.Users.FindAsync(userDto.Id);

        // Проверка на null - пользователь может не существовать
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userDto.Id} not found");

        // Обновляем свойства существующего объекта
        // Entity Framework отслеживает изменения автоматически
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        user.Role = userDto.Role;

        // Обновляем пароль только если он передан
        // string.IsNullOrEmpty() - проверяет на null и пустую строку
        if (!string.IsNullOrEmpty(userDto.Password))
            user.Password = AuthService.HashPassword(userDto.Password);

        // Сохраняем изменения в базу данных
        await _context.SaveChangesAsync();

        return MapToResponseDto(user);
    }

    /// <summary>
    /// Удаляет пользователя из базы данных.
    /// </summary>
    public async Task DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);

        // Удаляем только если пользователь найден
        if (user != null)
        {
            // DbSet.Remove() - помечает сущность для удаления
            _context.Users.Remove(user);

            // Фактическое удаление происходит здесь
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    public async Task<UserResponseDto?> GetByIdAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);

        // TERNARY OPERATOR (Тернарный оператор):
        // условие ? значение_если_true : значение_если_false
        return user == null ? null : MapToResponseDto(user);
    }

    /// <summary>
    /// Получает список всех пользователей.
    /// </summary>
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        // ToListAsync() - выполняет SQL запрос и возвращает список
        // Это момент, когда запрос реально отправляется в базу данных
        var users = await _context.Users.ToListAsync();

        // LINQ Select() - преобразует каждый элемент коллекции
        // Здесь: User -> UserResponseDto
        return users.Select(MapToResponseDto);
    }

    /// <summary>
    /// Преобразует Entity в DTO.
    ///
    /// MAPPING (Маппинг):
    /// Преобразование одного типа объекта в другой.
    /// В реальных проектах используют AutoMapper для автоматизации.
    ///
    /// STATIC METHOD:
    /// Метод не использует поля экземпляра (this), поэтому может быть static.
    /// Static методы можно вызывать без создания экземпляра класса.
    /// </summary>
    private static UserResponseDto MapToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Role = user.Role.ToString()  // Enum -> String
        };
    }
}
