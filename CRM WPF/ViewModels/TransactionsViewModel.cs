using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CRM_WPF.Helpers;
using CRM_WPF.Models;
using CRM_WPF.Services;

namespace CRM_WPF.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly DataService _dataService;
        private readonly CsvExportService _csvExportService;
        private readonly ExcelExportService _excelExportService;

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set { _transactions = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); SearchTransactions(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand ExportToCsvCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        public TransactionsViewModel(DataService dataService, CsvExportService csvExportService, ExcelExportService excelExportService)
        {
            _dataService = dataService;
            _csvExportService = csvExportService;
            _excelExportService = excelExportService;

            Transactions = new ObservableCollection<Transaction>();

            SearchCommand = new RelayCommand(_ => SearchTransactions());
            AddTransactionCommand = new RelayCommand(async _ => await AddTransactionAsync());

            ExportToCsvCommand = new RelayCommand(_ => ExportToCsv());
            ExportToExcelCommand = new RelayCommand(_ => ExportToExcel());

            //Pobranie transakcji z API przy starcie ViewModelu
            _ = LoadTransactionsAsync();
        }

        private async Task LoadTransactionsAsync()
        {
            await _dataService.LoadTransactionsAsync();
            Transactions = new ObservableCollection<Transaction>(_dataService.Transactions);
        }

        private void SearchTransactions()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Transactions = new ObservableCollection<Transaction>(_dataService.Transactions);
            }
            else
            {
                Transactions = new ObservableCollection<Transaction>(
                    _dataService.Transactions
                        .Where(t => t.Customer.Name.ToLower().Contains(SearchText.ToLower()) ||
                                    t.Product.Name.ToLower().Contains(SearchText.ToLower()))
                );
            }
        }

        private async Task AddTransactionAsync()
        {
            if (!_dataService.Customers.Any() || !_dataService.Products.Any())
            {
                MessageBox.Show("Brak klientów lub produktów w systemie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTransaction = new Transaction
            {
                CustomerId = _dataService.Customers.First().Id,
                ProductId = _dataService.Products.First().Id,
                Value = 0,
                Date = DateTime.Now,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Quantity = 1,
                Category = "Nieokreślona"
            };

            bool success = await _dataService.AddTransactionAsync(newTransaction);
            if (success)
            {
                await LoadTransactionsAsync(); //Odświeżenie listy transakcji po dodaniu nowej
            }
            else
            {
                MessageBox.Show("Błąd podczas dodawania transakcji!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToCsv()
        {
            if (!Transactions.Any())
            {
                MessageBox.Show("Brak danych do eksportu!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _csvExportService.ExportToCsv(Transactions.ToList());
        }

        private void ExportToExcel()
        {
            if (!Transactions.Any())
            {
                MessageBox.Show("Brak danych do eksportu!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _excelExportService.ExportToExcel(Transactions.ToList());
        }
    }
}
