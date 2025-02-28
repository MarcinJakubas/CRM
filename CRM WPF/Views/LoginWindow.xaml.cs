using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using CRM_WPF.Helpers;
using CRM_WPF.Models;
using CRM_WPF.Services;

namespace CRM_WPF.Views
{
    public partial class LoginWindow : Window
    {
        private readonly HttpClient _httpClient;
        public LoginWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginData = new { Username = txtUsername.Text, Password = txtPassword.Password };
            var response = await _httpClient.PostAsJsonAsync($"{AppConfig.ApiBaseUrl}/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserSession>();
                UserSession.CurrentUser = user;
                MessageBox.Show($"Witaj, {user.Username}!", "Zalogowano", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędny login lub hasło!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}