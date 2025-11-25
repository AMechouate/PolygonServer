using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
        var uri = new Uri(rawConnectionString);
        var userInfo = uri.UserInfo.Split(':');
        var user = userInfo.Length > 0 ? Uri.UnescapeDataString(userInfo[0]) : "";
        var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : "";
        var host = uri.Host;
        var dbPort = uri.Port > 0 ? uri.Port : 5432;
        var database = uri.AbsolutePath.TrimStart('/');
        
        // Build Npgsql connection string
        connectionString = $"Host={host};Port={dbPort};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
    catch
    {
        // If parsing fails, use original string
        connectionString = rawConnectionString;
    }
}

builder.Services.AddDbContext<PolygonDbContext>(options =>
{
    if (isPostgres)
    {
        // PostgreSQL (for Railway/Render)
        options.UseNpgsql(connectionString);
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
