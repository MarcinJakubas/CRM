using CRM_Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Pobranie Connection String z appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Rejestracja DbContext z Connection String
builder.Services.AddDbContext<CRMDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();