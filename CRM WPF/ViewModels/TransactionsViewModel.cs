using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CRM_WPF.Helpers;
using CRM_WPF.Models;
using CRM_WPF.Services;

namespace CRM_WPF.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
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
            set { _searchText = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand ExportToCsvCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        private readonly CsvExportService _csvExportService;
        private readonly ExcelExportService _excelExportService;

        public TransactionsViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(DataService.Instance.Transactions);
            SearchCommand = new RelayCommand(_ => SearchTransactions());
            AddTransactionCommand = new RelayCommand(_ => AddTransaction());

            // Inicjalizacja usług eksportu
            _csvExportService = new CsvExportService();
            _excelExportService = new ExcelExportService();

            // Komendy eksportu
            ExportToCsvCommand = new RelayCommand(_ => ExportToCsv());
            ExportToExcelCommand = new RelayCommand(_ => ExportToExcel());
        }

        private void ExportToCsv()
        {
            _csvExportService.ExportToCsv(Transactions.ToList());
        }

        private void ExportToExcel()
        {
            _excelExportService.ExportToExcel(Transactions.ToList());
        }

        private void SearchTransactions()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Transactions = new ObservableCollection<Transaction>(DataService.Instance.Transactions);
            }
            else
            {
                Transactions = new ObservableCollection<Transaction>(
                    DataService.Instance.Transactions
                        .Where(t => t.Customer.Name.ToLower().Contains(SearchText.ToLower()) ||
                                    t.Product.Name.ToLower().Contains(SearchText.ToLower()))
                );
            }
        }

        private void AddTransaction()
        {
            var newTransaction = new Transaction
            {
                Id = DataService.Instance.Transactions.Count + 1,
                Customer = DataService.Instance.Customers.FirstOrDefault(),
                Product = DataService.Instance.Products.FirstOrDefault(),
                Value = 0,
                Date = DateTime.Now,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Quantity = 1,
                Category = "Nieokreślona"
            };

            DataService.Instance.Transactions.Add(newTransaction);
            Transactions.Add(newTransaction);
        }
    }
}
