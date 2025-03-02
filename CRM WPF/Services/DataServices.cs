using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using CRM_WPF.Helpers;
using CRM_WPF.Models;

namespace CRM_WPF.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = AppConfig.ApiBaseUrl;

        public ObservableCollection<Customer> Customers { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Transaction> Transactions { get; private set; }

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            Customers = new ObservableCollection<Customer>();
            Products = new ObservableCollection<Product>();
            Transactions = new ObservableCollection<Transaction>();

            // Jeśli użytkownik jest zalogowany, dodaj nagłówek do każdego żądania
            if (UserSession.CurrentUser != null)
            {
                _httpClient.DefaultRequestHeaders.Add("User-Id", UserSession.CurrentUser.Id.ToString());
                _httpClient.DefaultRequestHeaders.Add("Username", UserSession.CurrentUser.Username);
            }
        }

        public async Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<Customer[]>($"{_apiBaseUrl}/customers");
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
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas pobierania klientów: {ex.Message}");
            }
        }
        public async Task LoadProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<Product[]>($"{_apiBaseUrl}/products");
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
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas pobierania produktów: {ex.Message}");
            }
        }
        public async Task LoadTransactionsAsync()
        {
            try
            {
                var transactions = await _httpClient.GetFromJsonAsync<Transaction[]>($"{_apiBaseUrl}/transactions");
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
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas pobierania transakcji: {ex.Message}");
            }
        }
        public async Task<bool> AddCustomerAsync(Customer newCustomer)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/customers", newCustomer);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas dodawania klienta: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddProductAsync(Product newProduct)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/products", newProduct);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas dodawania produktu: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddTransactionAsync(Transaction newTransaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/transactions", newTransaction);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas dodawania transakcji: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/customers/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas usuwania klienta: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"customers/{customer.Id}", customer);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                Console.WriteLine($"Błąd podczas aktualizacji danych klienta:{ex.Message}");
                return false;
            }
        }

    }
}
