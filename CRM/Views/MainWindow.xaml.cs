using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavMenu.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedTag = selectedItem.Tag.ToString();

                //switch (selectedTag)
                //{
                //    case "Dashboard":
                //        MainContent.Content = new DashboardView(); // Wczytaj widok dashboardu
                //        break;
                //    case "Clients":
                //        MainContent.Content = new ClientsView(); // Wczytaj widok klientów
                //        break;
                //    case "Contacts":
                //        MainContent.Content = new ContactsView();
                //        break;
                //    case "Tasks":
                //        MainContent.Content = new TasksView();
                //        break;
                //    case "Calendar":
                //        MainContent.Content = new CalendarView();
                //        break;
                //    case "Reports":
                //        MainContent.Content = new ReportsView();
                //        break;
                //    case "Settings":
                //        MainContent.Content = new SettingsView();
                //        break;
                //}
            }
        }
    }
}
