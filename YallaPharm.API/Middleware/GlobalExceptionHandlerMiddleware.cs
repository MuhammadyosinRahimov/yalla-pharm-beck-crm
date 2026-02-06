// ==============================================
// ГЛОБАЛЬНЫЙ ОБРАБОТЧИК ИСКЛЮЧЕНИЙ (Global Exception Handler)
// ==============================================
// Этот middleware перехватывает все необработанные исключения в приложении
// и преобразует их в единообразные HTTP-ответы с JSON-структурой.
// Обеспечивает централизованное логирование ошибок.
// ==============================================

using System.Net;
using System.Text.Json;
using Serilog;
using YallaPharm.Application.Exceptions;

namespace YallaPharm.API.Middleware;

/// <summary>
/// Middleware для глобальной обработки исключений.
/// Перехватывает все ошибки и возвращает унифицированный JSON-ответ.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    // Делегат для вызова следующего middleware в конвейере
    private readonly RequestDelegate _next;

    // Логгер для записи информации об ошибках
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Конструктор middleware.
    /// </summary>
    /// <param name="next">Следующий middleware в конвейере (pipeline)</param>
    /// <param name="logger">Логгер для записи ошибок</param>
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Основной метод middleware. Вызывается для каждого HTTP-запроса.
    /// </summary>
    /// <param name="context">HTTP-контекст запроса</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Пытаемся выполнить следующий middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            // Перехватываем любое исключение и обрабатываем его
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Обрабатывает исключение и формирует HTTP-ответ с ошибкой.
    /// </summary>
    /// <param name="context">HTTP-контекст</param>
    /// <param name="exception">Перехваченное исключение</param>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Получаем информацию о запросе для логирования
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        var traceId = context.TraceIdentifier;

        // Определяем HTTP-код и сообщение на основе типа исключения
        var (statusCode, errorCode, message, errors) = GetErrorDetails(exception);

        // Логируем ошибку с разным уровнем в зависимости от типа
        LogException(exception, requestPath, requestMethod, traceId, statusCode);

        // Формируем JSON-ответ
        var response = new ErrorResponse
        {
            TraceId = traceId,
            StatusCode = statusCode,
            ErrorCode = errorCode,
            Message = message,
            Errors = errors,
            Timestamp = DateTime.UtcNow
        };

        // Устанавливаем заголовки ответа
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = statusCode;

        // Сериализуем и отправляем ответ
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }

    /// <summary>
    /// Определяет детали ошибки на основе типа исключения.
    /// </summary>
    /// <param name="exception">Исключение</param>
    /// <returns>Кортеж с кодом статуса, кодом ошибки, сообщением и словарём ошибок</returns>
    private static (int StatusCode, string ErrorCode, string Message, IDictionary<string, string[]>? Errors)
        GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            // Кастомные исключения приложения
            AppException appEx => (
                appEx.StatusCode,
                appEx.ErrorCode,
                appEx.Message,
                appEx is ValidationException validationEx ? validationEx.Errors : null
            ),

            // Стандартные исключения .NET
            ArgumentNullException argNullEx => (
                400,
                "ARGUMENT_NULL",
                $"Параметр '{argNullEx.ParamName}' не может быть null.",
                null
            ),

            ArgumentException argEx => (
                400,
                "ARGUMENT_ERROR",
                argEx.Message,
                null
            ),

            KeyNotFoundException => (
                404,
                "NOT_FOUND",
                "Запрашиваемый ресурс не найден.",
                null
            ),

            UnauthorizedAccessException => (
                401,
                "UNAUTHORIZED",
                "Доступ запрещён. Требуется аутентификация.",
                null
            ),

            InvalidOperationException invOpEx => (
                400,
                "INVALID_OPERATION",
                invOpEx.Message,
                null
            ),

            OperationCanceledException => (
                499,
                "REQUEST_CANCELLED",
                "Запрос был отменён клиентом.",
                null
            ),

            // Все остальные исключения - внутренняя ошибка сервера
            _ => (
                500,
                "INTERNAL_ERROR",
                "Произошла внутренняя ошибка сервера. Пожалуйста, попробуйте позже.",
                null
            )
        };
    }

    /// <summary>
    /// Логирует исключение с соответствующим уровнем важности.
    /// </summary>
    private void LogException(Exception exception, string path, string method, string traceId, int statusCode)
    {
        var logMessage = "HTTP {Method} {Path} - TraceId: {TraceId} - Status: {StatusCode} - Exception: {ExceptionType}";

        if (statusCode >= 500)
        {
            // Критические ошибки (500+) - Error уровень с полным stack trace
            _logger.LogError(exception, logMessage, method, path, traceId, statusCode, exception.GetType().Name);
        }
        else if (statusCode >= 400)
        {
            // Клиентские ошибки (4xx) - Warning уровень
            _logger.LogWarning(logMessage + " - Message: {Message}",
                method, path, traceId, statusCode, exception.GetType().Name, exception.Message);
        }
    }
}

/// <summary>
/// Модель JSON-ответа при ошибке.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Уникальный идентификатор запроса для отслеживания.
    /// </summary>
    public string TraceId { get; set; } = string.Empty;

    /// <summary>
    /// HTTP-код статуса ответа.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Код ошибки для программной обработки на клиенте.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Человекочитаемое сообщение об ошибке.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Словарь ошибок валидации (опционально).
    /// </summary>
    public IDictionary<string, string[]>? Errors { get; set; }

    /// <summary>
    /// Время возникновения ошибки (UTC).
    /// </summary>
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Расширение для регистрации middleware в конвейере.
/// </summary>
public static class GlobalExceptionHandlerMiddlewareExtensions
{
    /// <summary>
    /// Добавляет глобальный обработчик исключений в конвейер middleware.
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    /// <returns>Построитель приложения для цепочки вызовов</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
