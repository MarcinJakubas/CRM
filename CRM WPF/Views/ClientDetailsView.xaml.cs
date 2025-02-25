using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CRM_WPF.Models;
using System.Collections.Generic;

namespace CRM_WPF.Views
{
    public partial class ClientDetailsView : UserControl
    {
        private Customer _customer;
        private List<Transaction> _transactions; 

        public ClientDetailsView(Customer customer, List<Transaction> allTransactions)
        {
            InitializeComponent();
            _customer = customer;
            _transactions = allTransactions.Where(t => t.CustomerId == customer.Id).ToList(); 
            LoadCustomerDetails();
            LoadClientTransactions();
        }

        private void LoadCustomerDetails()
        {
            txtName.Text = _customer.Name;
            txtEmail.Text = _customer.Email;
            txtPhone.Text = _customer.Phone;
            txtAddress.Text = _customer.Address;
            txtRegisteredDate.Text = _customer.RegistrationDate.ToShortDateString();
            txtStatus.Text = "Aktywny";  
            txtNotes.Text = _customer.Notes;
            txtAdditionalInfo.Text = _customer.AdditionalInfo;
        }

        private void LoadClientTransactions()
        {
            TransactionsDataGrid.ItemsSource = _transactions;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dane klienta zostały zapisane!", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Anulowano zmiany.", "Anulowano", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
