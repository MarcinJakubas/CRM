using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CRM_WPF.Models;
using CRM_WPF.Services;
using CRM_WPF.Helpers;

namespace CRM_WPF.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); SearchCustomers(); }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddClientCommand { get; }
        public ICommand OpenClientDetailsCommand { get; }

        public ClientsViewModel(DataService dataService)
        {
            _dataService = dataService;
            Customers = new ObservableCollection<Customer>();

            SearchCommand = new RelayCommand(_ => SearchCustomers());
            AddClientCommand = new RelayCommand(async _ => await AddClientAsync());
            OpenClientDetailsCommand = new RelayCommand(obj => OpenClientDetails(obj));

            //Pobranie klientów z API przy starcie ViewModelu
            _ = LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            await _dataService.LoadCustomersAsync();
            Customers = new ObservableCollection<Customer>(_dataService.Customers);
        }

        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Customers = new ObservableCollection<Customer>(_dataService.Customers);
            }
            else
            {
                Customers = new ObservableCollection<Customer>(
                    _dataService.Customers
                        .Where(c => c.Name.ToLower().Contains(SearchText.ToLower()) ||
                                    c.Company.ToLower().Contains(SearchText.ToLower()) ||
                                    c.City.ToLower().Contains(SearchText.ToLower()))
                );
            }
        }

        private async Task AddClientAsync()
        {
            var newClient = new Customer
            {
                Name = "Nowy Klient",
                Company = "",
                Email = "",
                Phone = "",
                Address = "",
                City = "",
                ZipCode = "",
                Country = "",
                RegistrationDate = System.DateTime.Now,
                LastPurchaseDate = System.DateTime.Now
            };

            bool success = await _dataService.AddCustomerAsync(newClient);
            if (success)
            {
                await LoadCustomersAsync(); //Odswieżenie listy po dodaniu klienta
            }
            else
            {
                MessageBox.Show("Błąd podczas dodawania klienta!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenClientDetails(object obj)
        {
            if (obj is Customer customer)
            {
                MessageBox.Show($"Otwieranie szczegółów dla:\n\n{customer.Name}",
                                "Szczegóły klienta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
