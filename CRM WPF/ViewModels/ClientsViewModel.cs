using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CRM_WPF.Models;
using CRM_WPF.Services;
using CRM_WPF.Helpers;

namespace CRM_WPF.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
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
            set { _searchText = value; OnPropertyChanged(); }
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

        public ClientsViewModel()
        {
            Customers = new ObservableCollection<Customer>(DataService.Instance.Customers);

            // 🔥 Poprawione wywołanie RelayCommand
            SearchCommand = new RelayCommand(_ => SearchCustomers());
            AddClientCommand = new RelayCommand(_ => AddClient());
            OpenClientDetailsCommand = new RelayCommand(obj => OpenClientDetails(obj));
        }

        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Customers = new ObservableCollection<Customer>(DataService.Instance.Customers);
            }
            else
            {
                Customers = new ObservableCollection<Customer>(
                    DataService.Instance.Customers
                        .Where(c => c.Name.ToLower().Contains(SearchText.ToLower()) ||
                                    c.Company.ToLower().Contains(SearchText.ToLower()) ||
                                    c.City.ToLower().Contains(SearchText.ToLower()))
                );
            }
        }

        private void AddClient()
        {
            var newClient = new Customer
            {
                Id = DataService.Instance.Customers.Count + 1,
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

            DataService.Instance.Customers.Add(newClient);
            Customers.Add(newClient);
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
