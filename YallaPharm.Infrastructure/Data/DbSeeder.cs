using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YallaPharm.Domain.Entities;
using YallaPharm.Domain.Enums;

namespace YallaPharm.Infrastructure.Data;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(ApplicationDbContext context, ILogger<DbSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            // Check if database is accessible
            if (!await _context.Database.CanConnectAsync())
            {
                _logger.LogWarning("Cannot connect to database. Skipping seeding.");
                return;
            }

            // Ensure database schema is up to date
            await EnsureDatabaseSchemaAsync();

            // Seed default admin user
            await SeedDefaultAdminAsync();

            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task EnsureDatabaseSchemaAsync()
    {
        _logger.LogInformation("Checking database schema...");

        // Create missing reference tables
        var createTablesSql = @"
            -- payment_methods
            CREATE TABLE IF NOT EXISTS payment_methods (
                id character varying(36) NOT NULL,
                name character varying(100) NOT NULL,
                CONSTRAINT ""PK_payment_methods"" PRIMARY KEY (id)
            );

            -- application_methods
            CREATE TABLE IF NOT EXISTS application_methods (
                id character varying(36) NOT NULL,
                application_method character varying(100) NOT NULL,
                CONSTRAINT ""PK_application_methods"" PRIMARY KEY (id)
            );

            -- for_whom
            CREATE TABLE IF NOT EXISTS for_whom (
                id character varying(36) NOT NULL,
                for_whom character varying(100) NOT NULL,
                CONSTRAINT ""PK_for_whom"" PRIMARY KEY (id)
            );

            -- organs_and_systems
            CREATE TABLE IF NOT EXISTS organs_and_systems (
                id character varying(36) NOT NULL,
                organs_and_system character varying(100) NOT NULL,
                CONSTRAINT ""PK_organs_and_systems"" PRIMARY KEY (id)
            );

            -- packaging_materials
            CREATE TABLE IF NOT EXISTS packaging_materials (
                id character varying(36) NOT NULL,
                packaging_material character varying(100) NOT NULL,
                CONSTRAINT ""PK_packaging_materials"" PRIMARY KEY (id)
            );

            -- preparation_colors
            CREATE TABLE IF NOT EXISTS preparation_colors (
                id character varying(36) NOT NULL,
                preparation_color character varying(100) NOT NULL,
                CONSTRAINT ""PK_preparation_colors"" PRIMARY KEY (id)
            );

            -- preparation_materials
            CREATE TABLE IF NOT EXISTS preparation_materials (
                id character varying(36) NOT NULL,
                preparation_material character varying(100) NOT NULL,
                CONSTRAINT ""PK_preparation_materials"" PRIMARY KEY (id)
            );

            -- scope_of_applications
            CREATE TABLE IF NOT EXISTS scope_of_applications (
                id character varying(36) NOT NULL,
                scope_of_application character varying(100) NOT NULL,
                CONSTRAINT ""PK_scope_of_applications"" PRIMARY KEY (id)
            );

            -- time_of_applications
            CREATE TABLE IF NOT EXISTS time_of_applications (
                id character varying(36) NOT NULL,
                time_of_application character varying(100) NOT NULL,
                CONSTRAINT ""PK_time_of_applications"" PRIMARY KEY (id)
            );

            -- categories
            CREATE TABLE IF NOT EXISTS categories (
                id character varying(36) NOT NULL,
                name character varying(100) NOT NULL,
                parent_id character varying(36),
                CONSTRAINT ""PK_categories"" PRIMARY KEY (id)
            );

            -- packaging_types
            CREATE TABLE IF NOT EXISTS packaging_types (
                id character varying(36) NOT NULL,
                name character varying(100) NOT NULL,
                CONSTRAINT ""PK_packaging_types"" PRIMARY KEY (id)
            );

            -- packaging_units
            CREATE TABLE IF NOT EXISTS packaging_units (
                id character varying(36) NOT NULL,
                name character varying(100) NOT NULL,
                CONSTRAINT ""PK_packaging_units"" PRIMARY KEY (id)
            );

            -- release_forms
            CREATE TABLE IF NOT EXISTS release_forms (
                id character varying(36) NOT NULL,
                name character varying(100) NOT NULL,
                CONSTRAINT ""PK_release_forms"" PRIMARY KEY (id)
            );

            -- EF Migrations History
            CREATE TABLE IF NOT EXISTS ""__EFMigrationsHistory"" (
                ""MigrationId"" character varying(150) NOT NULL,
                ""ProductVersion"" character varying(32) NOT NULL,
                CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY (""MigrationId"")
            );

            -- Mark migration as applied
            INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
            VALUES ('20260204122242_InitialCreate', '7.0.10')
            ON CONFLICT (""MigrationId"") DO NOTHING;
        ";

        try
        {
            await _context.Database.ExecuteSqlRawAsync(createTablesSql);
            _logger.LogInformation("Database schema check completed");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error ensuring database schema (may be expected if tables exist)");
        }
    }

    private async Task SeedDefaultAdminAsync()
    {
        const string adminEmail = "admin@yallapharm.com";

        // Check if admin already exists
        var existingAdmin = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == adminEmail);

        if (existingAdmin != null)
        {
            _logger.LogInformation("Default admin user already exists");
            return;
        }

        // Create default admin user
        var admin = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Admin",
            LastName = "System",
            PhoneNumber = "+7 000 000 0000",
            Email = adminEmail,
            Password = HashPassword("Admin123!"),
            Role = UserRole.SuperAdmin
        };

        _context.Users.Add(admin);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Default admin user created successfully");
        _logger.LogInformation("Email: {Email}", adminEmail);
        _logger.LogInformation("Password: Admin123!");
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
