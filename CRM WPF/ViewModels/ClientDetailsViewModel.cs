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
    public class ClientDetailsViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set { _customer = value; OnPropertyChanged(); }
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(); }
        }

        private string _additionalInfo;
        public string AdditionalInfo
        {
            get => _additionalInfo;
            set { _additionalInfo = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Transaction> Transactions { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ClientDetailsViewModel(DataService dataService, Customer customer)
        {
            _dataService = dataService;
            Customer = customer;

            Notes = customer.Notes;
            AdditionalInfo = customer.AdditionalInfo;

            Transactions = new ObservableCollection<Transaction>(
                _dataService.Transactions.Where(t => t.CustomerId == customer.Id));

            SaveCommand = new RelayCommand(async _ => await SaveClientAsync());
            CancelCommand = new RelayCommand(_ => CancelChanges());
        }

        private async Task SaveClientAsync()
        {
            Customer.Notes = Notes;
            Customer.AdditionalInfo = AdditionalInfo;

            bool success = await _dataService.UpdateCustomerAsync(Customer);
            if (success)
            {
                MessageBox.Show("Dane klienta zostały zapisane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Błąd podczas zapisywania klienta.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelChanges()
        {
            Notes = Customer.Notes;
            AdditionalInfo = Customer.AdditionalInfo;
            MessageBox.Show("Anulowano zmiany.", "Anulowano", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
