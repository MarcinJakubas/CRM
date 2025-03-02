using CRM_Api.Data;
using CRM_Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Pobranie Connection String z appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Rejestracja DbContext z Connection String
builder.Services.AddDbContext<CRMDbContext>(options =>
    options.UseSqlServer(connectionString));

// Rejestracja HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<ErrorLogger>();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();