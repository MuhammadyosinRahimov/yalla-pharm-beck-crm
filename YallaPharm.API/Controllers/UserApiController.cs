using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

/// <summary>
/// API контроллер для управления пользователями.
///
/// CONTROLLER (Контроллер) - ASP.NET CORE WEB API:
///
/// Контроллер - это класс, который обрабатывает HTTP запросы.
/// Он является "входной точкой" для клиентов (браузер, мобильное приложение).
///
/// АТРИБУТЫ:
/// [ApiController] - включает автоматическую валидацию модели и другие фичи API
/// [Route("user")] - базовый путь для всех методов: /user
/// [Authorize] - требует аутентификации (JWT токен)
///
/// HTTP МЕТОДЫ:
/// - GET    - получить данные (чтение)
/// - POST   - создать новые данные
/// - PUT    - обновить существующие данные
/// - DELETE - удалить данные
///
/// ПРИМЕР ЗАПРОСОВ:
/// GET    /user       - получить всех пользователей
/// GET    /user/123   - получить пользователя с id=123
/// POST   /user       - создать нового пользователя
/// DELETE /user/123   - удалить пользователя
/// </summary>
[ApiController]
[Route("user")]
[Authorize(Roles = "Administrator,SuperAdmin")]  // Доступ только для админов
public class UserApiController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с пользователями.
    ///
    /// DEPENDENCY INJECTION:
    /// Контроллер не создаёт сервис сам - он получает его через конструктор.
    /// Это позволяет легко заменить реализацию и писать тесты.
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор контроллера.
    ///
    /// ASP.NET Core автоматически создаёт и передаёт IUserService.
    /// Это настраивается в Program.cs: builder.Services.AddScoped&lt;IUserService, UserService&gt;()
    /// </summary>
    public UserApiController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Создаёт нового пользователя.
    ///
    /// HTTP: POST /user
    ///
    /// [HttpPost] - атрибут, указывающий что метод обрабатывает POST запросы
    ///
    /// [FromBody] - указывает что данные приходят в теле запроса (JSON)
    /// ASP.NET автоматически десериализует JSON в объект UserDto
    ///
    /// IActionResult - базовый тип для HTTP ответов:
    /// - Ok(data)      -> HTTP 200 с данными
    /// - NotFound()    -> HTTP 404
    /// - BadRequest()  -> HTTP 400
    ///
    /// ПРИМЕР ЗАПРОСА:
    /// POST /user
    /// Content-Type: application/json
    /// {
    ///   "firstName": "Иван",
    ///   "lastName": "Иванов",
    ///   "email": "ivan@example.com",
    ///   "password": "secret123",
    ///   "role": 1
    /// }
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        // Вызываем сервис для создания пользователя
        var result = await _userService.CreateAsync(userDto);

        // Ok() - возвращает HTTP 200 с данными нового пользователя
        return Ok(result);
    }

    /// <summary>
    /// Обновляет данные пользователя.
    ///
    /// HTTP: POST /user/update
    ///
    /// Примечание: По REST должен быть PUT /user/{id},
    /// но здесь используется POST для совместимости с некоторыми клиентами.
    /// </summary>
    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UserDto userDto)
    {
        try
        {
            var result = await _userService.UpdateAsync(userDto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            // NotFound() - возвращает HTTP 404 (пользователь не найден)
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Удаляет пользователя.
    ///
    /// HTTP: DELETE /user/{id}
    ///
    /// [HttpDelete("{id}")] - "{id}" это параметр в URL
    /// Пример: DELETE /user/abc-123-def
    ///
    /// string id - автоматически извлекается из URL
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _userService.DeleteAsync(id);

        // Возвращаем сообщение об успешном удалении
        return Ok(new { message = "User deleted successfully" });
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    ///
    /// HTTP: GET /user/{id}
    ///
    /// Пример: GET /user/abc-123-def
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _userService.GetByIdAsync(id);

        // Если пользователь не найден - возвращаем 404
        if (result == null)
            return NotFound(new { message = "User not found" });

        return Ok(result);
    }

    /// <summary>
    /// Получает список всех пользователей.
    ///
    /// HTTP: GET /user
    ///
    /// [HttpGet] без параметров - метод доступен по базовому пути /user
    ///
    /// ПРИМЕР ОТВЕТА:
    /// [
    ///   { "id": "1", "firstName": "Иван", "lastName": "Иванов", "role": "Administrator" },
    ///   { "id": "2", "firstName": "Пётр", "lastName": "Петров", "role": "Operator" }
    /// ]
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }
}
