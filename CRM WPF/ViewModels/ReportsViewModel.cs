using CRM_WPF.Helpers;
using CRM_WPF.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CRM_WPF.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private Dictionary<int, string> _products;
        public Dictionary<int, string> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        private int _selectedProductId;
        public int SelectedProductId
        {
            get => _selectedProductId;
            set
            {
                _selectedProductId = value;
                OnPropertyChanged();
                _ = LoadProductDataAsync(value);
            }
        }

        public SeriesCollection SalesData { get; set; }
        public SeriesCollection ProductSalesData { get; set; }
        public SeriesCollection PieChartData { get; set; }
        public ICommand ProductSelectionChangedCommand { get; }

        public ReportsViewModel(DataService dataService)
        {
            _dataService = dataService;
            Products = new Dictionary<int, string>();
            SalesData = new SeriesCollection();
            ProductSalesData = new SeriesCollection();
            PieChartData = new SeriesCollection();

            ProductSelectionChangedCommand = new RelayCommand(_ => _ = LoadProductDataAsync(SelectedProductId));

            //Pobranie danych z API przy starcie ViewModelu
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                await _dataService.LoadTransactionsAsync();
                await _dataService.LoadProductsAsync();

                var transactions = _dataService.Transactions.ToList();
                var products = _dataService.Products.ToList();

                if (!transactions.Any())
                {
                    MessageBox.Show("Brak danych transakcji w API!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Grupowanie sprzedaży miesięcznej (dla wykresu słupkowego całkowitej sprzedaży)
                var salesByMonth = transactions
                    .GroupBy(t => t.Month)
                    .OrderBy(g => g.Key)
                    .Select(g => new { Month = g.Key, TotalSales = g.Sum(t => t.Value) })
                    .ToList();

                SalesData = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sprzedaż miesięczna",
                    Values = new ChartValues<double>(salesByMonth.Select(x => x.TotalSales)),
                    DataLabels = true
                }
            };

                OnPropertyChanged(nameof(SalesData));

                //Tworzenie mapowania produktów dla `ComboBox`
                Products = products.ToDictionary(p => p.Id, p => p.Name);
                OnPropertyChanged(nameof(Products));

                // Tworzenie wykresu kołowego z podziałem sprzedaży na produkty
                var groupedTransactions = transactions
                    .Where(t => t.Product != null)
                    .GroupBy(t => t.Product.Name)
                    .ToList();

                if (groupedTransactions.Any())
                {
                    PieChartData = new SeriesCollection();
                    foreach (var group in groupedTransactions)
                    {
                        PieChartData.Add(new PieSeries
                        {
                            Title = group.Key,
                            Values = new ChartValues<double> { group.Sum(t => t.Value) },
                            DataLabels = true
                        });
                    }
                }
                else
                {
                    PieChartData = new SeriesCollection();
                }
                OnPropertyChanged(nameof(PieChartData));
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadProductDataAsync(int productId)
        {
            try
            {
                if (productId == 0 || !_dataService.Products.Any(p => p.Id == productId))
                {
                    ProductSalesData = new SeriesCollection();
                    OnPropertyChanged(nameof(ProductSalesData));
                    return;
                }

                var transactions = _dataService.Transactions
                    .Where(t => t.Product != null && t.Product.Id == productId)
                    .ToList();

                if (!transactions.Any())
                {
                    ProductSalesData = new SeriesCollection();
                }
                else
                {
                    ProductSalesData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = $"Sprzedaż: {Products[productId]}",
                            Values = new ChartValues<double>(transactions.Select(x => x.Value)),
                            DataLabels = true
                        }
                    };
                }

                OnPropertyChanged(nameof(ProductSalesData));
            }
            catch (Exception ex)
            {
                await ErrorLogger.LogError(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
