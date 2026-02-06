// ==============================================
// СЕРВИС АУТЕНТИФИКАЦИИ (Authentication Service)
// ==============================================
// Отвечает за:
// - Вход пользователя (проверка email/пароля)
// - Генерацию JWT токенов
// - Хеширование паролей
// ==============================================

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Exceptions;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

/// <summary>
/// Сервис аутентификации пользователей.
/// Реализует интерфейс IAuthService.
/// </summary>
public class AuthService : IAuthService
{
    // ==== ПРИВАТНЫЕ ПОЛЯ (ЗАВИСИМОСТИ) ====
    // readonly - поле нельзя изменить после инициализации
    // _ (underscore) - конвенция для приватных полей

    /// <summary>
    /// Контекст базы данных для работы с таблицами.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Конфигурация приложения (доступ к appsettings.json).
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Логгер для записи событий аутентификации.
    /// </summary>
    private readonly ILogger<AuthService> _logger;

    // ==== КЭШИРОВАННЫЕ ЗНАЧЕНИЯ КОНФИГУРАЦИИ ====
    // Чтобы не читать из конфигурации при каждом запросе
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly int _tokenLifetimeDays;

    /// <summary>
    /// Конструктор с внедрением зависимостей (Dependency Injection).
    /// ASP.NET Core автоматически передаёт нужные объекты.
    /// </summary>
    /// <param name="context">Контекст БД</param>
    /// <param name="configuration">Конфигурация</param>
    /// <param name="logger">Логгер</param>
    public AuthService(
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;

        // ==== БЕЗОПАСНОЕ ЧТЕНИЕ КОНФИГУРАЦИИ ====
        // Читаем и валидируем настройки один раз при создании сервиса

        _jwtKey = _configuration["AuthOptions:Key"]
            ?? throw new ConfigurationException("AuthOptions:Key");

        _jwtIssuer = _configuration["AuthOptions:Issuer"] ?? "YallaPharmCRM";
        _jwtAudience = _configuration["AuthOptions:Audience"] ?? "YallaPharmCRMUsers";

        // Парсим с значением по умолчанию (30 дней)
        if (!int.TryParse(_configuration["AuthOptions:TokenLifetimeDays"], out _tokenLifetimeDays))
        {
            _tokenLifetimeDays = 30;
        }
    }

    /// <summary>
    /// Выполняет вход пользователя в систему.
    /// </summary>
    /// <param name="loginDto">Данные для входа (email и пароль)</param>
    /// <returns>
    /// Объект LoginResponseDto с токеном и данными пользователя,
    /// или null если аутентификация не удалась.
    /// </returns>
    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        // ==== ВАЛИДАЦИЯ ВХОДНЫХ ДАННЫХ ====
        if (string.IsNullOrWhiteSpace(loginDto.Email))
        {
            _logger.LogWarning("Login attempt with empty email");
            return null;
        }

        if (string.IsNullOrWhiteSpace(loginDto.Password))
        {
            _logger.LogWarning("Login attempt with empty password for email: {Email}", loginDto.Email);
            return null;
        }

        // ==== ПОИСК ПОЛЬЗОВАТЕЛЯ В БД ====
        // FirstOrDefaultAsync - асинхронный поиск первой записи или null
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        // ==== ПРОВЕРКА СУЩЕСТВОВАНИЯ И ПАРОЛЯ ====
        if (user == null)
        {
            // Не раскрываем, что пользователь не найден (безопасность)
            _logger.LogWarning("Login attempt for non-existent user: {Email}", loginDto.Email);
            return null;
        }

        if (!VerifyPassword(loginDto.Password, user.Password))
        {
            _logger.LogWarning("Invalid password attempt for user: {Email}", loginDto.Email);
            return null;
        }

        // ==== ГЕНЕРАЦИЯ JWT ТОКЕНА ====
        var token = GenerateJwtToken(user);

        _logger.LogInformation("User {Email} logged in successfully", user.Email);

        // ==== ФОРМИРОВАНИЕ ОТВЕТА ====
        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role.ToString()
        };
    }

    /// <summary>
    /// Генерирует JWT токен для аутентифицированного пользователя.
    /// </summary>
    /// <param name="user">Пользователь из БД</param>
    /// <returns>Строка JWT токена</returns>
    private string GenerateJwtToken(User user)
    {
        // ==== СОЗДАНИЕ КЛЮЧА ПОДПИСИ ====
        // SymmetricSecurityKey - симметричный ключ (один для шифрования и дешифрования)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

        // SigningCredentials - данные для подписи токена
        // HmacSha256 - алгоритм хеширования
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // ==== ФОРМИРОВАНИЕ CLAIMS (УТВЕРЖДЕНИЙ) ====
        // Claims - данные о пользователе, которые будут в токене
        var claims = new[]
        {
            // NameIdentifier - уникальный ID пользователя
            new Claim(ClaimTypes.NameIdentifier, user.Id),

            // Email - адрес электронной почты
            new Claim(ClaimTypes.Email, user.Email),

            // Name - полное имя пользователя
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),

            // Role - роль для авторизации
            new Claim(ClaimTypes.Role, user.Role.ToString()),

            // Дополнительные claims для удобства
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName)
        };

        // ==== СОЗДАНИЕ ТОКЕНА ====
        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,                         // Кто выдал токен
            audience: _jwtAudience,                     // Для кого токен
            claims: claims,                             // Данные пользователя
            notBefore: DateTime.UtcNow,                 // Действителен с этого момента
            expires: DateTime.UtcNow.AddDays(_tokenLifetimeDays),  // Срок действия
            signingCredentials: credentials             // Подпись
        );

        // ==== СЕРИАЛИЗАЦИЯ В СТРОКУ ====
        // JwtSecurityTokenHandler преобразует объект токена в строку
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Проверяет соответствие пароля хешу из БД.
    /// </summary>
    /// <param name="password">Введённый пароль</param>
    /// <param name="hashedPassword">Хеш из базы данных</param>
    /// <returns>true если пароль верный</returns>
    private static bool VerifyPassword(string password, string hashedPassword)
    {
        // Хешируем введённый пароль и сравниваем с сохранённым
        return HashPassword(password) == hashedPassword;
    }

    /// <summary>
    /// Хеширует пароль с использованием SHA256.
    /// Статический метод - не требует экземпляра класса.
    /// </summary>
    /// <param name="password">Исходный пароль</param>
    /// <returns>Хеш пароля в Base64</returns>
    public static string HashPassword(string password)
    {
        // using - автоматически освобождает ресурсы после использования
        // SHA256 - криптографический алгоритм хеширования
        using var sha256 = SHA256.Create();

        // Преобразуем строку в байты и хешируем
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Преобразуем байты в Base64 строку для хранения
        return Convert.ToBase64String(hashedBytes);
    }
}
