using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework - Support for both PostgreSQL (Railway) and MySQL/MariaDB
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? Environment.GetEnvironmentVariable("POSTGRES_URL")
    ?? Environment.GetEnvironmentVariable("POSTGRES_PRIVATE_URL")
    ?? "Server=/tmp/mysql.sock;Database=PolygonDb;User=adammechouate;Password=naima;Protocol=Unix;";

// Determine database type and normalize connection string
var isPostgres = rawConnectionString.Contains("postgres://", StringComparison.OrdinalIgnoreCase) ||
                 rawConnectionString.Contains("postgresql://", StringComparison.OrdinalIgnoreCase) ||
                 rawConnectionString.Contains("PostgreSQL", StringComparison.OrdinalIgnoreCase) ||
                 rawConnectionString.StartsWith("postgres", StringComparison.OrdinalIgnoreCase) ||
                 Environment.GetEnvironmentVariable("DATABASE_URL") != null ||
                 Environment.GetEnvironmentVariable("POSTGRES_URL") != null ||
                 Environment.GetEnvironmentVariable("POSTGRES_PRIVATE_URL") != null;

string connectionString = rawConnectionString;

// Convert PostgreSQL URL format to Npgsql connection string format if needed
if (isPostgres && rawConnectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
{
    try
    {
        // Parse postgresql://user:pass@host:port/db format
        // Example: postgresql://user:pass@host:5432/db
        var uri = new Uri(rawConnectionString);
        
        // Extract user and password from userinfo
        var userInfo = uri.UserInfo;
        string username = "";
        string password = "";
        
        if (!string.IsNullOrEmpty(userInfo))
        {
            var colonIndex = userInfo.IndexOf(':');
            if (colonIndex > 0)
            {
                username = Uri.UnescapeDataString(userInfo.Substring(0, colonIndex));
                password = Uri.UnescapeDataString(userInfo.Substring(colonIndex + 1));
            }
            else
            {
                username = Uri.UnescapeDataString(userInfo);
            }
        }
        
        var host = uri.Host;
        var dbPort = uri.Port > 0 ? uri.Port : 5432;
        var database = uri.AbsolutePath.TrimStart('/').Split('?')[0]; // Remove query string if present
        
        // Build Npgsql connection string with proper parameter names
        // Npgsql uses: Host, Port, Database, Username (not User!), Password
        connectionString = $"Host={host};Port={dbPort};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
        
        // Log for debugging
        Console.WriteLine($"[DB] PostgreSQL detected. Host={host}, Port={dbPort}, Database={database}, Username={username}");
    }
    catch (Exception ex)
    {
        // If parsing fails, log and try to build manually
        Console.WriteLine($"[DB] Error parsing connection string: {ex.Message}");
        Console.WriteLine($"[DB] Raw connection string: {rawConnectionString}");
        
        // Try to extract manually as fallback
        try
        {
            // Manual extraction as last resort
            var match = System.Text.RegularExpressions.Regex.Match(
                rawConnectionString, 
                @"postgresql://([^:]+):([^@]+)@([^/]+)/([^?]+)"
            );
            
            if (match.Success)
            {
                var username = Uri.UnescapeDataString(match.Groups[1].Value);
                var password = Uri.UnescapeDataString(match.Groups[2].Value);
                var hostPort = match.Groups[3].Value;
                var database = match.Groups[4].Value;
                
                var hostParts = hostPort.Split(':');
                var fallbackHost = hostParts[0];
                var fallbackPort = hostParts.Length > 1 ? int.Parse(hostParts[1]) : 5432;
                
                connectionString = $"Host={fallbackHost};Port={fallbackPort};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
                Console.WriteLine($"[DB] Manual extraction successful. Host={fallbackHost}, Port={fallbackPort}, Database={database}");
            }
            else
            {
                connectionString = rawConnectionString;
            }
        }
        catch
        {
            connectionString = rawConnectionString;
        }
    }
}

builder.Services.AddDbContext<PolygonDbContext>(options =>
{
    if (isPostgres)
    {
        // PostgreSQL (for Railway/Render)
        // Validate connection string before using it
        try
        {
            // Test if connection string is valid by creating a connection string builder
            var builder = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);
            Console.WriteLine($"[DB] PostgreSQL connection string validated. Host={builder.Host}, Database={builder.Database}, Username={builder.Username}");
            options.UseNpgsql(connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DB] ERROR: Invalid PostgreSQL connection string: {ex.Message}");
            Console.WriteLine($"[DB] Connection string: {connectionString.Substring(0, Math.Min(50, connectionString.Length))}...");
            throw; // Re-throw to fail fast
        }
    }
    else
    {
        // MySQL/MariaDB (for local development)
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(11, 2, 3)));
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger immer aktivieren (auch in Production für API-Dokumentation)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polygon API V1");
    c.RoutePrefix = "swagger"; // Swagger UI unter /swagger
    c.DisplayRequestDuration();
});

app.UseCors("AllowAll");

// HTTPS Redirection nur in Production aktivieren
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PolygonDbContext>();
    context.Database.EnsureCreated();
}

// Get port from environment variable (for cloud hosting)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5104";
app.Run($"http://0.0.0.0:{port}");
