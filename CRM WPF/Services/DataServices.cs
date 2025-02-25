using System;
using System.Collections.ObjectModel;
using CRM_WPF.Models;

namespace CRM_WPF.Services
{
    public class DataService
    {
        private static DataService _instance;
        private static readonly object _lock = new object();

        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataService();
                        }
                    }
                }
                return _instance;
            }
        }

        public ObservableCollection<Customer> Customers { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Transaction> Transactions { get; private set; }

        private DataService()
        {
            LoadData(); 
        }

        private void LoadData()
        {
            //Testowe dane 
            Customers = new ObservableCollection<Customer>
            {
                new Customer { Id = 1, Name = "Jan Kowalski", Email = "jan.kowalski@example.com", Phone = "123-456-789",
                               Company = "SoftTech", Address = "ul. Główna 10", City = "Warszawa", ZipCode = "00-001",
                               Country = "Polska", RegistrationDate = new DateTime(2020, 5, 10), LastPurchaseDate = new DateTime(2024, 2, 10) },

                new Customer { Id = 2, Name = "Anna Nowak", Email = "anna.nowak@example.com", Phone = "987-654-321",
                               Company = "MediaCorp", Address = "ul. Krótka 15", City = "Kraków", ZipCode = "30-002",
                               Country = "Polska", RegistrationDate = new DateTime(2021, 3, 5), LastPurchaseDate = new DateTime(2024, 1, 25) },

                new Customer { Id = 3, Name = "Piotr Wiśniewski", Email = "piotr.wisniewski@example.com", Phone = "555-555-555",
                               Company = "InfoSoft", Address = "ul. Zielona 3", City = "Poznań", ZipCode = "61-003",
                               Country = "Polska", RegistrationDate = new DateTime(2019, 11, 20), LastPurchaseDate = new DateTime(2024, 3, 12) }
            };

            Products = new ObservableCollection<Product>
            {
                new Product { Id = 101, Name = "Laptop", Category = "Elektronika", Price = 4500, StockQuantity = 10 },
                new Product { Id = 102, Name = "Smartphone", Category = "Elektronika", Price = 3200, StockQuantity = 15 },
                new Product { Id = 103, Name = "Pralka", Category = "AGD", Price = 1800, StockQuantity = 5 },
                new Product { Id = 104, Name = "Telewizor", Category = "RTV", Price = 2500, StockQuantity = 7 },
                new Product { Id = 105, Name = "Słuchawki bezprzewodowe", Category = "Elektronika", Price = 400, StockQuantity = 30 },
                new Product { Id = 106, Name = "Rower górski", Category = "Sport", Price = 3000, StockQuantity = 3 }
            };

            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction { Id = 1, Customer = Customers[0], Product = Products[0], Value = 4500, Date = new DateTime(2024, 1, 15),
                                  Month = 1, Year = 2024, Quantity = 1, Category = "Elektronika" },

                new Transaction { Id = 2, Customer = Customers[1], Product = Products[1], Value = 6400, Date = new DateTime(2024, 1, 20),
                                  Month = 1, Year = 2024, Quantity = 2, Category = "Elektronika" },

                new Transaction { Id = 3, Customer = Customers[2], Product = Products[2], Value = 1800, Date = new DateTime(2024, 2, 5),
                                  Month = 2, Year = 2024, Quantity = 1, Category = "AGD" },

                new Transaction { Id = 4, Customer = Customers[0], Product = Products[3], Value = 2500, Date = new DateTime(2024, 3, 22),
                                  Month = 3, Year = 2024, Quantity = 1, Category = "RTV" },

                new Transaction { Id = 5, Customer = Customers[1], Product = Products[4], Value = 800, Date = new DateTime(2024, 4, 18),
                                  Month = 4, Year = 2024, Quantity = 2, Category = "Elektronika" },

                new Transaction { Id = 6, Customer = Customers[2], Product = Products[5], Value = 3000, Date = new DateTime(2024, 5, 10),
                                  Month = 5, Year = 2024, Quantity = 1, Category = "Sport" }
            };
        }
    }
}
