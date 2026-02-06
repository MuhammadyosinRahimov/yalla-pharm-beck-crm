// ==============================================
// API TEST RUNNER
// ==============================================
// Автоматическое тестирование всех API endpoints
// с проверкой на ошибки 500 (Internal Server Error)
// ==============================================

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace YallaPharm.API.Scripts;

/// <summary>
/// Результат тестирования одного endpoint
/// </summary>
public class TestResult
{
    public string Method { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public long ElapsedMs { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
    public bool IsServerError => StatusCode >= 500;
    public bool IsClientError => StatusCode >= 400 && StatusCode < 500;
}

/// <summary>
/// Определение endpoint для тестирования
/// </summary>
public class EndpointDefinition
{
    public string Method { get; set; } = "GET";
    public string Path { get; set; } = string.Empty;
    public object? Body { get; set; }
    public bool RequiresAuth { get; set; } = true;
    public string Category { get; set; } = "GENERAL";
}

/// <summary>
/// Консольный раннер для автоматического тестирования API endpoints
/// </summary>
public class ApiTestRunner
{
    private readonly HttpClient _client;
    private string? _authToken;
    private string _baseUrl = "http://localhost:5071";

    // Статистика
    private int _passed = 0;
    private int _failed = 0;
    private readonly List<TestResult> _errors500 = new();
    private readonly List<TestResult> _allResults = new();

    // Тестовые данные (из DbSeeder)
    private const string TestEmail = "admin@yallapharm.com";
    private const string TestPassword = "Admin123!";

    // Список всех endpoints для тестирования (54 endpoints)
    private readonly List<EndpointDefinition> _endpoints = new()
    {
        // ===== AUTH (1 endpoint) =====
        new() { Category = "AUTH", Method = "POST", Path = "/api/login", Body = new { email = TestEmail, password = TestPassword }, RequiresAuth = false },

        // ===== USERS (4 endpoints) =====
        new() { Category = "USER", Method = "GET", Path = "/user" },
        new() { Category = "USER", Method = "GET", Path = "/user/test-user-id" },
        new() { Category = "USER", Method = "POST", Path = "/user/update", Body = new { id = "test-id", firstName = "Test", lastName = "User" } },
        // POST /user и DELETE /user/{id} - пропускаем чтобы не создавать/удалять данные

        // ===== CLIENTS (4 endpoints) =====
        new() { Category = "CLIENT", Method = "GET", Path = "/api/clients" },
        new() { Category = "CLIENT", Method = "GET", Path = "/api/clients/test-client-id" },
        new() { Category = "CLIENT", Method = "POST", Path = "/api/clients/update", Body = new { id = "test-id", firstName = "Test" } },
        // POST /api/clients и DELETE /api/clients/{id} - пропускаем

        // ===== ORDERS (16 endpoints) =====
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/1/10" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/2/10" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/3/10" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/4/10" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/5/10" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/by-state/6/10" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/by-state", Body = new { orderState = 1, count = 10 } },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/test-order-id" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/order-number/test-order-id" },
        new() { Category = "ORDER", Method = "GET", Path = "/api/orders/pharmacy-orders/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/update-state", Body = new { orderId = "test-id", state = 1 } },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/consulting", Body = new { clientId = "test-id" } },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/insearch/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/waiting-client/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/placement/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/delivered/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/cancellation/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/rejection/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/return-from-rejection/test-order-id" },
        new() { Category = "ORDER", Method = "POST", Path = "/api/orders/return-products/test-order-id" },

        // ===== PHARMACIES (4 endpoints) =====
        new() { Category = "PHARMACY", Method = "GET", Path = "/api/pharmacy" },
        new() { Category = "PHARMACY", Method = "GET", Path = "/api/pharmacy/test-pharmacy-id" },
        new() { Category = "PHARMACY", Method = "POST", Path = "/api/pharmacy/update", Body = new { id = "test-id", name = "Test" } },
        // POST и DELETE - пропускаем

        // ===== PRODUCTS (4 endpoints) =====
        new() { Category = "PRODUCT", Method = "GET", Path = "/api/product" },
        new() { Category = "PRODUCT", Method = "GET", Path = "/api/product/test-product-id" },
        new() { Category = "PRODUCT", Method = "POST", Path = "/api/product/update", Body = new { id = "test-id", name = "Test" } },
        // POST и DELETE - пропускаем

        // ===== PRODUCT TEMPLATES (5 endpoints) =====
        new() { Category = "TEMPLATE", Method = "GET", Path = "/api/productTemplate" },
        new() { Category = "TEMPLATE", Method = "GET", Path = "/api/productTemplate/test-template-id" },
        new() { Category = "TEMPLATE", Method = "POST", Path = "/api/productTemplate/update", Body = new { id = "test-id", name = "Test" } },
        // POST, DELETE и POST /image - пропускаем

        // ===== PACKAGING TYPE (4 endpoints) =====
        new() { Category = "PKG_TYPE", Method = "GET", Path = "/api/packagingType" },
        new() { Category = "PKG_TYPE", Method = "GET", Path = "/api/packagingType/test-id" },
        new() { Category = "PKG_TYPE", Method = "POST", Path = "/api/packagingType/update", Body = new { id = "test-id", name = "Test" } },

        // ===== PACKAGING UNIT (4 endpoints) =====
        new() { Category = "PKG_UNIT", Method = "GET", Path = "/api/packagingUnit" },
        new() { Category = "PKG_UNIT", Method = "GET", Path = "/api/packagingUnit/test-id" },
        new() { Category = "PKG_UNIT", Method = "POST", Path = "/api/packagingUnit/update", Body = new { id = "test-id", name = "Test" } },

        // ===== PAYMENT METHOD (4 endpoints) =====
        new() { Category = "PAYMENT", Method = "GET", Path = "/api/paymentMethod" },
        new() { Category = "PAYMENT", Method = "GET", Path = "/api/paymentMethod/test-id" },
        new() { Category = "PAYMENT", Method = "POST", Path = "/api/paymentMethod/update", Body = new { id = "test-id", name = "Test" } },

        // ===== RELEASE FORM (4 endpoints) =====
        new() { Category = "REL_FORM", Method = "GET", Path = "/api/releaseForm" },
        new() { Category = "REL_FORM", Method = "GET", Path = "/api/releaseForm/test-id" },
        new() { Category = "REL_FORM", Method = "POST", Path = "/api/releaseForm/update", Body = new { id = "test-id", name = "Test" } },

        // ===== DELIVERY (2 endpoints - anonymous) =====
        new() { Category = "DELIVERY", Method = "POST", Path = "/api/delivery/send", Body = new { orderId = "test-id" }, RequiresAuth = false },
        new() { Category = "DELIVERY", Method = "POST", Path = "/api/delivery/update", Body = new { orderId = "test-id" }, RequiresAuth = false },

        // ===== TELEGRAM (2 endpoints - anonymous) =====
        new() { Category = "TELEGRAM", Method = "GET", Path = "/telegram", RequiresAuth = false },
        new() { Category = "TELEGRAM", Method = "POST", Path = "/telegram", Body = new { update_id = 0 }, RequiresAuth = false },
    };

    public ApiTestRunner(string? baseUrl = null)
    {
        if (!string.IsNullOrEmpty(baseUrl))
        {
            _baseUrl = baseUrl;
        }

        _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    /// <summary>
    /// Запуск всех тестов
    /// </summary>
    public async Task RunAllTests()
    {
        PrintHeader();

        // Шаг 1: Аутентификация
        Console.WriteLine("\n[STEP 1] Authenticating...");
        var authSuccess = await Authenticate();
        if (!authSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Authentication failed! Some tests will be skipped.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Authentication successful!");
            Console.ResetColor();
        }

        // Шаг 2: Тестирование endpoints
        Console.WriteLine($"\n[STEP 2] Testing {_endpoints.Count} endpoints...\n");
        Console.WriteLine(new string('-', 80));

        foreach (var endpoint in _endpoints)
        {
            // Пропускаем endpoints требующие авторизации, если нет токена
            if (endpoint.RequiresAuth && string.IsNullOrEmpty(_authToken))
            {
                PrintSkipped(endpoint);
                continue;
            }

            var result = await TestEndpoint(endpoint);
            _allResults.Add(result);
            PrintResult(result, endpoint.Category);

            // Небольшая задержка между запросами
            await Task.Delay(50);
        }

        // Шаг 3: Вывод итогов
        PrintSummary();
    }

    /// <summary>
    /// Аутентификация и получение JWT токена
    /// </summary>
    public async Task<bool> Authenticate()
    {
        try
        {
            var loginData = new { email = TestEmail, password = TestPassword };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_baseUrl}/api/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseBody);

                // Пытаемся найти токен в разных возможных полях (Token, token, accessToken)
                if (doc.RootElement.TryGetProperty("token", out var tokenElement))
                {
                    _authToken = tokenElement.GetString();
                }
                else if (doc.RootElement.TryGetProperty("Token", out var tokenElement2))
                {
                    _authToken = tokenElement2.GetString();
                }
                else if (doc.RootElement.TryGetProperty("accessToken", out var accessTokenElement))
                {
                    _authToken = accessTokenElement.GetString();
                }

                if (!string.IsNullOrEmpty(_authToken))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _authToken);
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Auth error: {ex.Message}");
            Console.ResetColor();
            return false;
        }
    }

    /// <summary>
    /// Тестирование одного endpoint
    /// </summary>
    public async Task<TestResult> TestEndpoint(EndpointDefinition endpoint)
    {
        var result = new TestResult
        {
            Method = endpoint.Method,
            Path = endpoint.Path
        };

        var stopwatch = Stopwatch.StartNew();

        try
        {
            HttpResponseMessage response;
            var url = $"{_baseUrl}{endpoint.Path}";

            switch (endpoint.Method.ToUpper())
            {
                case "GET":
                    response = await _client.GetAsync(url);
                    break;

                case "POST":
                    var postContent = endpoint.Body != null
                        ? new StringContent(JsonSerializer.Serialize(endpoint.Body), Encoding.UTF8, "application/json")
                        : new StringContent("{}", Encoding.UTF8, "application/json");
                    response = await _client.PostAsync(url, postContent);
                    break;

                case "PUT":
                    var putContent = endpoint.Body != null
                        ? new StringContent(JsonSerializer.Serialize(endpoint.Body), Encoding.UTF8, "application/json")
                        : new StringContent("{}", Encoding.UTF8, "application/json");
                    response = await _client.PutAsync(url, putContent);
                    break;

                case "DELETE":
                    response = await _client.DeleteAsync(url);
                    break;

                default:
                    throw new NotSupportedException($"HTTP method {endpoint.Method} is not supported");
            }

            stopwatch.Stop();
            result.StatusCode = (int)response.StatusCode;
            result.ElapsedMs = stopwatch.ElapsedMilliseconds;

            // Читаем тело ответа для ошибок
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(errorBody))
                {
                    // Для 500 ошибок показываем больше информации
                    var maxLength = result.IsServerError ? 1000 : 500;
                    result.ErrorMessage = errorBody.Length > maxLength
                        ? errorBody.Substring(0, maxLength) + "..."
                        : errorBody;
                }
            }

            // Подсчёт статистики
            if (result.IsSuccess)
            {
                _passed++;
            }
            else
            {
                _failed++;
                if (result.IsServerError)
                {
                    _errors500.Add(result);
                }
            }
        }
        catch (TaskCanceledException)
        {
            stopwatch.Stop();
            result.StatusCode = 0;
            result.ElapsedMs = stopwatch.ElapsedMilliseconds;
            result.ErrorMessage = "Request timeout";
            _failed++;
        }
        catch (HttpRequestException ex)
        {
            stopwatch.Stop();
            result.StatusCode = 0;
            result.ElapsedMs = stopwatch.ElapsedMilliseconds;
            result.ErrorMessage = ex.Message;
            _failed++;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            result.StatusCode = 0;
            result.ElapsedMs = stopwatch.ElapsedMilliseconds;
            result.ErrorMessage = ex.Message;
            _failed++;
        }

        return result;
    }

    /// <summary>
    /// Вывод заголовка
    /// </summary>
    private void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════════╗
║                       YALLA PHARM API TEST RUNNER                             ║
║                                                                               ║
║  Автоматическое тестирование всех API endpoints                              ║
║  Проверка на ошибки 500 (Internal Server Error)                              ║
╚═══════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"\nBase URL: {_baseUrl}");
        Console.WriteLine($"Total endpoints to test: {_endpoints.Count}");
    }

    /// <summary>
    /// Вывод результата одного теста
    /// </summary>
    private void PrintResult(TestResult result, string category)
    {
        // Формируем строку с выравниванием
        var categoryStr = $"[{category}]".PadRight(12);
        var methodStr = result.Method.PadRight(6);
        var pathStr = result.Path.PadRight(40);
        var timeStr = $"({result.ElapsedMs}ms)".PadLeft(10);

        Console.Write($"{categoryStr} {methodStr} {pathStr} ");

        // Статус с цветом
        if (result.IsSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"✓ {result.StatusCode}");
        }
        else if (result.IsServerError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"✗ {result.StatusCode}");
        }
        else if (result.IsClientError)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"⚠ {result.StatusCode}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"✗ ERR");
        }

        Console.ResetColor();
        Console.WriteLine($" {timeStr}");

        // Вывод сообщения об ошибке для 500
        if (!string.IsNullOrEmpty(result.ErrorMessage) && result.IsServerError)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // Показываем первые 200 символов ошибки
            var errorPreview = result.ErrorMessage.Length > 200
                ? result.ErrorMessage.Substring(0, 200) + "..."
                : result.ErrorMessage;
            Console.WriteLine($"             Error: {errorPreview}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Вывод пропущенного теста
    /// </summary>
    private void PrintSkipped(EndpointDefinition endpoint)
    {
        var categoryStr = $"[{endpoint.Category}]".PadRight(12);
        var methodStr = endpoint.Method.PadRight(6);
        var pathStr = endpoint.Path.PadRight(40);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{categoryStr} {methodStr} {pathStr} SKIPPED (no auth)");
        Console.ResetColor();
    }

    /// <summary>
    /// Вывод итоговой статистики
    /// </summary>
    private void PrintSummary()
    {
        Console.WriteLine(new string('=', 80));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════════╗
║                              TEST RESULTS                                     ║
╚═══════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        var total = _passed + _failed;
        var passRate = total > 0 ? (double)_passed / total * 100 : 0;

        Console.WriteLine($"\n  Total:       {total} endpoints tested");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  Passed:      {_passed} ({passRate:F1}%)");
        Console.ResetColor();

        if (_failed > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Failed:      {_failed} ({100 - passRate:F1}%)");
            Console.ResetColor();
        }

        Console.ForegroundColor = _errors500.Count > 0 ? ConsoleColor.Red : ConsoleColor.Green;
        Console.WriteLine($"  Errors 500:  {_errors500.Count}");
        Console.ResetColor();

        // Список ошибок 500
        if (_errors500.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          FAILED ENDPOINTS (500)                              ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            for (int i = 0; i < _errors500.Count; i++)
            {
                var error = _errors500[i];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {i + 1}. {error.Method} {error.Path}");
                Console.ResetColor();

                if (!string.IsNullOrEmpty(error.ErrorMessage))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"     Error: {error.ErrorMessage}");
                    Console.ResetColor();
                }
            }
        }

        // Финальный статус
        Console.WriteLine();
        if (_errors500.Count == 0 && _failed == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✓ ALL TESTS PASSED! No 500 errors detected.");
        }
        else if (_errors500.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ⚠ No 500 errors, but some requests failed with 4xx.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ {_errors500.Count} ENDPOINT(S) RETURNED 500 ERROR!");
        }
        Console.ResetColor();
        Console.WriteLine();
    }
}
