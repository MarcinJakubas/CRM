using System.Windows.Controls;
using CRM_WPF.ViewModels;

namespace CRM_WPF.Views
{
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
            DataContext = new ReportsViewModel();
        }
    }
}
