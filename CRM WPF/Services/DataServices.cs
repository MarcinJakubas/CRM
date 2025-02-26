using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CRM_WPF.Models;

namespace CRM_WPF.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private const string API_BASE_URL = "http://localhost:5000/api"; // Zmienić na adres testowy

        public ObservableCollection<Customer> Customers { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Transaction> Transactions { get; private set; }

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            Customers = new ObservableCollection<Customer>();
            Products = new ObservableCollection<Product>();
            Transactions = new ObservableCollection<Transaction>();
        }

        public async Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<Customer[]>($"{API_BASE_URL}/customers");
                if (customers != null)
                {
                    Customers.Clear();
                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania klientów: {ex.Message}");
            }
        }
        public async Task LoadProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<Product[]>($"{API_BASE_URL}/products");
                if (products != null)
                {
                    Products.Clear();
                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania produktów: {ex.Message}");
            }
        }
        public async Task LoadTransactionsAsync()
        {
            try
            {
                var transactions = await _httpClient.GetFromJsonAsync<Transaction[]>($"{API_BASE_URL}/transactions");
                if (transactions != null)
                {
                    Transactions.Clear();
                    foreach (var transaction in transactions)
                    {
                        Transactions.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania transakcji: {ex.Message}");
            }
        }
        public async Task<bool> AddCustomerAsync(Customer newCustomer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{API_BASE_URL}/customers", newCustomer);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas dodawania klienta: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddProductAsync(Product newProduct)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{API_BASE_URL}/products", newProduct);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas dodawania produktu: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddTransactionAsync(Transaction newTransaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{API_BASE_URL}/transactions", newTransaction);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas dodawania transakcji: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{API_BASE_URL}/customers/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas usuwania klienta: {ex.Message}");
                return false;
            }
        }
    }
}
