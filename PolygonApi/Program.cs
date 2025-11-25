using Microsoft.EntityFrameworkCore;
using PolygonApi.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework - Support for both PostgreSQL (Railway) and MySQL/MariaDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? Environment.GetEnvironmentVariable("POSTGRES_URL")
    ?? Environment.GetEnvironmentVariable("POSTGRES_PRIVATE_URL")
    ?? "Server=/tmp/mysql.sock;Database=PolygonDb;User=adammechouate;Password=naima;Protocol=Unix;";

// Determine database type based on connection string
var isPostgres = connectionString.Contains("postgres://", StringComparison.OrdinalIgnoreCase) ||
                 connectionString.Contains("postgresql://", StringComparison.OrdinalIgnoreCase) ||
                 connectionString.Contains("PostgreSQL", StringComparison.OrdinalIgnoreCase) ||
                 connectionString.StartsWith("postgres", StringComparison.OrdinalIgnoreCase) ||
                 Environment.GetEnvironmentVariable("DATABASE_URL") != null ||
                 Environment.GetEnvironmentVariable("POSTGRES_URL") != null ||
                 Environment.GetEnvironmentVariable("POSTGRES_PRIVATE_URL") != null;

builder.Services.AddDbContext<PolygonDbContext>(options =>
{
    if (isPostgres)
    {
        // PostgreSQL (for Railway/Render)
        // Railway provides DATABASE_URL in format: postgresql://user:pass@host:port/db
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
