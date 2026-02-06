// ==============================================
// ПОЛЬЗОВАТЕЛЬСКИЕ ИСКЛЮЧЕНИЯ (Custom Exceptions)
// ==============================================
// Этот файл содержит кастомные классы исключений для обработки ошибок в приложении.
// Каждый класс наследуется от базового Exception и предоставляет дополнительную
// информацию для правильной обработки HTTP-ответов.
// ==============================================

namespace YallaPharm.Application.Exceptions;

/// <summary>
/// Базовый класс для всех кастомных исключений приложения.
/// Содержит HTTP-код ответа для middleware обработки.
/// </summary>
public abstract class AppException : Exception
{
    /// <summary>
    /// HTTP-код статуса для ответа клиенту.
    /// (HTTP status code for client response)
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Код ошибки для идентификации типа исключения.
    /// (Error code for exception type identification)
    /// </summary>
    public string ErrorCode { get; }

    protected AppException(string message, int statusCode, string errorCode)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}

/// <summary>
/// Исключение: Сущность не найдена (404 Not Found).
/// Используется когда запрашиваемый объект не существует в базе данных.
/// </summary>
public class NotFoundException : AppException
{
    public NotFoundException(string entityName, string id)
        : base($"Сущность '{entityName}' с идентификатором '{id}' не найдена.",
               404, "NOT_FOUND")
    { }

    public NotFoundException(string message)
        : base(message, 404, "NOT_FOUND")
    { }
}

/// <summary>
/// Исключение: Ошибка валидации (400 Bad Request).
/// Используется когда входные данные не соответствуют требованиям.
/// </summary>
public class ValidationException : AppException
{
    /// <summary>
    /// Словарь ошибок валидации (поле -> список ошибок).
    /// (Validation errors dictionary: field -> list of errors)
    /// </summary>
    public IDictionary<string, string[]>? Errors { get; }

    public ValidationException(string message)
        : base(message, 400, "VALIDATION_ERROR")
    { }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Ошибка валидации данных.", 400, "VALIDATION_ERROR")
    {
        Errors = errors;
    }
}

/// <summary>
/// Исключение: Не авторизован (401 Unauthorized).
/// Используется когда пользователь не аутентифицирован.
/// </summary>
public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message = "Требуется аутентификация.")
        : base(message, 401, "UNAUTHORIZED")
    { }
}

/// <summary>
/// Исключение: Доступ запрещён (403 Forbidden).
/// Используется когда у пользователя нет прав на выполнение операции.
/// </summary>
public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "Доступ к ресурсу запрещён.")
        : base(message, 403, "FORBIDDEN")
    { }
}

/// <summary>
/// Исключение: Конфликт данных (409 Conflict).
/// Используется при конфликте данных (например, дублирование уникального поля).
/// </summary>
public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message, 409, "CONFLICT")
    { }
}

/// <summary>
/// Исключение: Неправильная конфигурация (500 Internal Server Error).
/// Используется когда в настройках приложения отсутствуют обязательные параметры.
/// </summary>
public class ConfigurationException : AppException
{
    public ConfigurationException(string configKey)
        : base($"Отсутствует обязательный параметр конфигурации: '{configKey}'.",
               500, "CONFIGURATION_ERROR")
    { }
}

/// <summary>
/// Исключение: Ошибка внешнего сервиса (502 Bad Gateway).
/// Используется при ошибках взаимодействия с внешними API.
/// </summary>
public class ExternalServiceException : AppException
{
    public ExternalServiceException(string serviceName, string message)
        : base($"Ошибка при обращении к сервису '{serviceName}': {message}",
               502, "EXTERNAL_SERVICE_ERROR")
    { }
}

/// <summary>
/// Исключение: Ошибка работы с файлами (500 Internal Server Error).
/// Используется при ошибках чтения/записи файлов.
/// </summary>
public class FileOperationException : AppException
{
    public FileOperationException(string operation, string fileName, Exception? innerException = null)
        : base($"Ошибка операции '{operation}' с файлом '{fileName}'. " +
               (innerException != null ? $"Причина: {innerException.Message}" : ""),
               500, "FILE_OPERATION_ERROR")
    { }
}

/// <summary>
/// Исключение: Бизнес-логика нарушена (422 Unprocessable Entity).
/// Используется когда операция невозможна из-за бизнес-правил.
/// </summary>
public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message)
        : base(message, 422, "BUSINESS_RULE_VIOLATION")
    { }
}
