using System.Windows.Controls;
using CRM_WPF.ViewModels;

namespace CRM_WPF.Views
{
    public partial class TransactionsView : UserControl
    {
        public TransactionsView()
        {
            InitializeComponent();
            DataContext = new TransactionsViewModel(); 
        }
    }
}
