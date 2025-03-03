using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using CRM_WPF.Services;
using CRM_WPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CRM_WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataService _dataService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(DataService dataService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dataService = dataService;
            _serviceProvider = serviceProvider;

            // Pobierz dane z API na start
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await _dataService.LoadCustomersAsync();
            await _dataService.LoadProductsAsync();
            await _dataService.LoadTransactionsAsync();
        }

        private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavMenu.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedTag = selectedItem.Tag?.ToString();

                if (!string.IsNullOrEmpty(selectedTag))
                {
                    switch (selectedTag)
                    {
                        case "Dashboard":
                            MainContent.Content = _serviceProvider.GetRequiredService<DashboardView>();
                            break;
                        case "Clients":
                            MainContent.Content = _serviceProvider.GetRequiredService<ClientsView>();
                            break;
                        case "Transaction":
                            MainContent.Content = _serviceProvider.GetRequiredService<TransactionsView>();
                            break;
                        case "Calendar":
                            MainContent.Content = _serviceProvider.GetRequiredService<CalendarView>();
                            break;
                        case "Reports":
                            MainContent.Content = _serviceProvider.GetRequiredService<ReportsView>();
                            break;
                        case "Admin":
                            MainContent.Content = _serviceProvider.GetRequiredService<AdminView>();
                            break;
                    }
                }
            }
        }
    }
}
