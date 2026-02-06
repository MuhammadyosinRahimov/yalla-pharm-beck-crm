// ==============================================
// ТОЧКА ВХОДА ПРИЛОЖЕНИЯ (Application Entry Point)
// ==============================================
// Program.cs - главный файл запуска ASP.NET Core приложения.
// Здесь настраиваются все сервисы, middleware и конфигурация.
// ==============================================

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using YallaPharm.API.Middleware;
using YallaPharm.API.Scripts;
using YallaPharm.Application;
using YallaPharm.Infrastructure;
using YallaPharm.Infrastructure.Data;

// ==== ПРОВЕРКА РЕЖИМА ТЕСТИРОВАНИЯ API ====
// Если передан аргумент --test-api, запускаем тест-раннер вместо сервера
if (args.Contains("--test-api"))
{
    Console.WriteLine("Starting API Test Runner...");

    // Получаем URL из аргументов или используем по умолчанию
    var urlIndex = Array.IndexOf(args, "--url");
    var baseUrl = urlIndex >= 0 && args.Length > urlIndex + 1
        ? args[urlIndex + 1]
        : "http://localhost:5071";

    var runner = new ApiTestRunner(baseUrl);
    await runner.RunAllTests();
    return;
}

// ==== СОЗДАНИЕ BUILDER ====
// Builder отвечает за конфигурацию сервисов ДО запуска приложения
var builder = WebApplication.CreateBuilder(args);

// ==== НАСТРОЙКА SERILOG ====
// Serilog - популярная библиотека для структурированного логирования
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Читаем настройки из appsettings.json
    .Enrich.FromLogContext()                        // Добавляем контекст в логи
    .CreateLogger();

builder.Host.UseSerilog();

// ==== ВАЛИДАЦИЯ КОНФИГУРАЦИИ JWT ====
// Проверяем наличие обязательных параметров ПЕРЕД запуском приложения
var jwtKey = builder.Configuration["AuthOptions:Key"];
var jwtIssuer = builder.Configuration["AuthOptions:Issuer"];
var jwtAudience = builder.Configuration["AuthOptions:Audience"];

if (string.IsNullOrEmpty(jwtKey))
{
    Log.Fatal("КРИТИЧЕСКАЯ ОШИБКА: Отсутствует AuthOptions:Key в конфигурации!");
    throw new InvalidOperationException("JWT Key is not configured. Please set 'AuthOptions:Key' in appsettings.json");
}

if (jwtKey.Length < 32)
{
    Log.Fatal("КРИТИЧЕСКАЯ ОШИБКА: AuthOptions:Key должен быть минимум 32 символа!");
    throw new InvalidOperationException("JWT Key must be at least 32 characters long");
}

if (string.IsNullOrEmpty(jwtIssuer))
{
    Log.Warning("AuthOptions:Issuer не настроен, используется значение по умолчанию");
    jwtIssuer = "YallaPharmCRM";
}

if (string.IsNullOrEmpty(jwtAudience))
{
    Log.Warning("AuthOptions:Audience не настроен, используется значение по умолчанию");
    jwtAudience = "YallaPharmCRMUsers";
}

// ==== РЕГИСТРАЦИЯ КОНТРОЛЛЕРОВ ====
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Игнорируем циклические ссылки при сериализации
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // Не включаем null-значения в JSON-ответы
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddEndpointsApiExplorer();

// ==== НАСТРОЙКА SWAGGER ====
// Swagger - инструмент для документации и тестирования API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Yalla Pharm CRM API",
        Version = "v1",
        Description = "API for Yalla Pharm CRM System"
    });

    // Настройка авторизации через Bearer Token в Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }
    });
});

// ==== НАСТРОЙКА JWT АУТЕНТИФИКАЦИИ ====
// JWT (JSON Web Token) - стандарт для безопасной передачи данных
builder.Services.AddAuthentication(options =>
{
    // Устанавливаем JWT как схему по умолчанию
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,                          // Проверять издателя токена
        ValidIssuer = jwtIssuer,                        // Допустимый издатель
        ValidateAudience = true,                        // Проверять аудиторию
        ValidAudience = jwtAudience,                    // Допустимая аудитория
        ValidateLifetime = true,                        // Проверять срок действия
        IssuerSigningKey = new SymmetricSecurityKey(    // Ключ для подписи
            Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuerSigningKey = true,                // Проверять подпись
        ClockSkew = TimeSpan.Zero                       // Без допуска по времени
    };

    // Обработка событий аутентификации для логирования
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Log.Warning("JWT Authentication failed: {Error}", context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Log.Debug("JWT Token validated for user: {User}",
                context.Principal?.Identity?.Name ?? "Unknown");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// ==== НАСТРОЙКА CORS ====
// CORS (Cross-Origin Resource Sharing) - политика безопасности для браузеров
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",    // Next.js development
                "http://localhost:5173",    // Vite development
                "https://yalla-pharm-crm.vercel.app"      // Production
            )
            .AllowAnyMethod()               // Разрешаем все HTTP методы
            .AllowAnyHeader()               // Разрешаем все заголовки
            .AllowCredentials();            // Разрешаем передачу cookies
    });
});

// ==== РЕГИСТРАЦИЯ СЛОЁВ ПРИЛОЖЕНИЯ ====
// Подключаем сервисы из Application и Infrastructure слоёв
builder.Services.AddApplication();                              // Бизнес-логика
builder.Services.AddInfrastructure(builder.Configuration);      // Доступ к данным

// ==== РЕГИСТРАЦИЯ SEEDER ====
// DbSeeder - заполняет БД начальными данными
builder.Services.AddScoped<DbSeeder>();

// ==== СОЗДАНИЕ ПРИЛОЖЕНИЯ ====
var app = builder.Build();

// ==== ИНИЦИАЛИЗАЦИЯ БАЗЫ ДАННЫХ ====
// Применяем миграции и выполняем seed данных при запуске
using (var scope = app.Services.CreateScope())
{
    try
    {
        // Автоматическое применение миграций
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Log.Information("Applying database migrations...");
        db.Database.Migrate();
        Log.Information("Database migrations applied successfully");

        // Выполняем seed данных
        var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await seeder.SeedAsync();
        Log.Information("Database seeding completed successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error occurred during database initialization");
        // Не прерываем запуск - приложение может работать без seed данных
    }
}

// ==== НАСТРОЙКА MIDDLEWARE PIPELINE ====
// Порядок middleware КРИТИЧЕСКИ важен!

// 1. Глобальный обработчик исключений (должен быть ПЕРВЫМ)
app.UseGlobalExceptionHandler();

// 2. Swagger UI (доступен всегда для удобства разработки)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yalla Pharm CRM API v1");
    c.RoutePrefix = "swagger";
});

// 3. Логирование запросов через Serilog
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.ToString());
    };
});

// 4. CORS - должен быть перед Authentication
app.UseCors("AllowFrontend");

// 5. Аутентификация и авторизация
app.UseAuthentication();
app.UseAuthorization();

// 6. Статические файлы (для изображений и т.д.)
app.UseStaticFiles();

// 7. Маршрутизация контроллеров
app.MapControllers();

// ==== ЗАПУСК ПРИЛОЖЕНИЯ ====
Log.Information("Yalla Pharm CRM API starting on {Urls}", string.Join(", ", app.Urls));

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
