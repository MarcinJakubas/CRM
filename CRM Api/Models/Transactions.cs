using CRM_Api.Models;

namespace CRM_Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int Month { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}