using System.Windows.Controls;
using CRM_WPF.ViewModels;

namespace CRM_WPF.Views
{
    public partial class ClientsView : UserControl
    {
        public ClientsView(ClientsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

