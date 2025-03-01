using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CRM_Api.Models;

namespace CRM_Api.Data
{
    public class CRMDbContext : DbContext
    {
        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> Errors { get; set; }
    }
}