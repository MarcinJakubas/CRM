using CRM_WPF.Helpers;
using CRM_WPF.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CRM_WPF.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
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
                LoadProductData(value);
            }
        }

        public SeriesCollection SalesData { get; set; }
        public SeriesCollection ProductSalesData { get; set; }
        public SeriesCollection PieChartData { get; set; }
        public ICommand ProductSelectionChangedCommand { get; }

        public ReportsViewModel()
        {
            LoadData();
            ProductSelectionChangedCommand = new RelayCommand(_ => LoadProductData(SelectedProductId));
        }

        private void LoadData()
        {
            var transactions = DataService.Instance.Transactions;

            if (transactions == null || !transactions.Any())
            {
                MessageBox.Show("Brak danych transakcji w DataService!");
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
            Products = transactions
                .Select(t => new { t.Product.Id, t.Product.Name })
                .Distinct()
                .ToDictionary(x => x.Id, x => x.Name);

            OnPropertyChanged(nameof(Products));

            var groupedTransactions = transactions
                .Where(t => t.Product != null)
                .GroupBy(t => t.Product.Name)
                .ToList();

            if (groupedTransactions.Any())
            {
                var newSeriesCollection = new SeriesCollection();

                foreach (var group in groupedTransactions)
                {
                    newSeriesCollection.Add(new PieSeries
                    {
                        Title = group.Key,
                        Values = new ChartValues<double> { group.Sum(t => t.Value) },
                        DataLabels = true
                    });
                }

                PieChartData = newSeriesCollection;
            }
            else
            {
                PieChartData = new SeriesCollection();
            }

            OnPropertyChanged(nameof(PieChartData));
        }


        private void LoadProductData(int productId)
        {
            var transactions = DataService.Instance.Transactions
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
    }
}
