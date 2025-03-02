using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CRM_Api.Data;
using CRM_Api.Models;

namespace CRM_Api.Services
{
    public class AuditService
    {
        private readonly CRMDbContext _context;

        public AuditService(CRMDbContext context)
        {
            _context = context;
        }

        public async Task LogChangeAsync(string tableName, string columnName, string oldValue, string newValue, string operation, int? userId, string username)
        {
            var log = new AuditLog
            {
                TableName = tableName,
                ColumnName = columnName,
                OldValue = oldValue,
                NewValue = newValue,
                OperationType = operation,
                UserId = userId,
                Username = username
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
