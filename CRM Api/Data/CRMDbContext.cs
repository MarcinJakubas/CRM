using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CRM_Api.Models;
using CRM_Api.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using System.Diagnostics;

namespace CRM_Api.Data
{
    public class CRMDbContext : DbContext
    {
        private readonly AuditService _auditService;
        private readonly ErrorLogger _errorLogger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CRMDbContext(DbContextOptions<CRMDbContext> options, AuditService auditService, ErrorLogger errorLogger, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _auditService = auditService;
            _errorLogger = errorLogger;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> Errors { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            int? userId = null;
            string username = "Unknown";

            if (httpContext?.Request.Headers.ContainsKey("User-Id") == true)
                userId = int.TryParse(httpContext.Request.Headers["User-Id"], out int uid) ? uid : (int?)null;

            if (httpContext?.Request.Headers.ContainsKey("Username") == true)
                username = httpContext.Request.Headers["Username"];

            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted || e.State == EntityState.Added)
                .ToList();

            foreach (var entry in entries)
            {
                string tableName = entry.Entity.GetType().Name;
                string operation = entry.State.ToString();

                foreach (var property in entry.OriginalValues.Properties)
                {
                    var oldValue = entry.OriginalValues[property]?.ToString();
                    var newValue = entry.CurrentValues[property]?.ToString();

                    try
                    {
                        if (operation == "Added")
                        {
                            await _auditService.LogChangeAsync(tableName, property.Name, null, newValue, "INSERT", userId, username);
                        }
                        else if (operation == "Modified" && oldValue != newValue)
                        {
                            await _auditService.LogChangeAsync(tableName, property.Name, oldValue, newValue, "UPDATE", userId, username);
                        }
                        else if (operation == "Deleted")
                        {
                            await _auditService.LogChangeAsync(tableName, property.Name, oldValue, null, "DELETE", userId, username);
                        }
                    }
                    catch (Exception ex)
                    {
                        await _errorLogger.LogErrorAsync($"Audit Error: {ex.Message}", ex.StackTrace, userId, username);
                    }
                }
            }

            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync($"Database Save Error: {ex.Message}", ex.StackTrace, userId, username);
                throw;
            }
        }
    }
}
