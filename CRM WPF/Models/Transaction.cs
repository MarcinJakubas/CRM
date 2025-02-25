using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_WPF.Models
{
    public class Transaction
    {
        public int Id { get; set; } // Unikalne ID transakcji
        public int CustomerId { get; set; } // ID Klienta
        public Customer Customer { get; set; } // Relacja do Klienta
        public int ProductId { get; set; } // ID Towaru
        public Product Product { get; set; } // Relacja do Produktu
        public double Value { get; set; } // Wartość transakcji
        public DateTime Date { get; set; } // Data transakcji
        public int Month { get; set; } // Miesiąc sprzedaży (1-12)
        public int Year { get; set; } // Rok sprzedaży
        public int Quantity { get; set; } // Ilość zakupionych sztuk
        public string Category { get; set; } // Kategoria produktu
    }
}
