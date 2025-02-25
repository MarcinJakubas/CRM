using System.Windows;
using System.Windows.Controls;

namespace CRM_WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new DashboardView(); // Domyślnie Dashboard na start
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
                            MainContent.Content = new DashboardView();
                            break;
                        case "Clients":
                            MainContent.Content = new ClientsView();
                            break;
                        case "Transaction":
                            MainContent.Content = new TransactionsView();
                            break;
                        case "Calendar":
                            MainContent.Content = new CalendarView();
                            break;
                        case "Reports":
                            MainContent.Content = new ReportsView();
                            break;
                    }
                }
            }
        }
    }
}
