using System.Windows.Controls;
using CRM_WPF.ViewModels;

namespace CRM_WPF.Views
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
            DataContext = new ClientsViewModel(); 
        }
    }
}
