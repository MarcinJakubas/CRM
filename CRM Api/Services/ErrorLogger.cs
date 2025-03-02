using System;
using System.Threading.Tasks;
using CRM_Api.Data;
using CRM_Api.Models;

namespace CRM_Api.Services
{
    public class ErrorLogger
    {
        private readonly CRMDbContext _context;

        public ErrorLogger(CRMDbContext context)
        {
            _context = context;
        }

        public async Task LogErrorAsync(string errorMessage, string stackTrace, int? userId = null, string username = "Unknown")
        {
            try
            {
                var errorLog = new ErrorLog
                {
                    ErrorMessage = errorMessage,
                    StackTrace = stackTrace,
                    UserId = userId,
                    Username = username,
                    ErrorTime = DateTime.UtcNow
                };

                _context.Errors.Add(errorLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Jeśli logowanie błędu samo powoduje błąd, wypisujemy go do konsoli
                Console.WriteLine($"[ErrorLogger] Failed to log error: {ex.Message}");
            }
        }
    }
}
