using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_WPF.Models
{
    public class Product
    {
        public int Id { get; set; } // Unikalny ID produktu
        public string Name { get; set; } // Nazwa produktu
        public string Category { get; set; } // Kategoria produktu (np. Elektronika, Odzież)
        public decimal Price { get; set; } // Cena jednostkowa
        public int StockQuantity { get; set; } // Ilość w magazynie
    }
}
