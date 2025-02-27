using CRM_WPF.Models;
using CRM_WPF.Services;
using CRM_WPF.ViewModels;
using System.Windows.Controls;

namespace CRM_WPF.Views
{
    public partial class ClientDetailsView : UserControl
    {
        public ClientDetailsView(DataService dataService, Customer customer)
        {
            InitializeComponent();
            DataContext = new ClientDetailsViewModel(dataService, customer);
        }
    }
}
