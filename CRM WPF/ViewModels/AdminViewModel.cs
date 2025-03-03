using CRM_WPF.Helpers;
using CRM_WPF.Models;
using CRM_WPF.Services;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRM_WPF.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<AuditLog> AuditLogs { get; set; }
        public ObservableCollection<ErrorLog> ErrorLogs { get; set; }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }
        public AdminViewModel()
        {
            Users = new ObservableCollection<User>();
            AuditLogs = new ObservableCollection<AuditLog>();
            ErrorLogs = new ObservableCollection<ErrorLog>();
        }

        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set { _connectionString = value; OnPropertyChanged(); }
        }

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand SaveConfigCommand { get; }

        public AdminViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            AddUserCommand = new RelayCommand(_ => AddUser());
            EditUserCommand = new RelayCommand(_ => EditUser());
            DeleteUserCommand = new RelayCommand(_ => DeleteUser());
            SaveConfigCommand = new RelayCommand(_ => SaveConnectionString());

            _ = LoadData();
        }

        private async Task LoadData()
        {
            Users = new ObservableCollection<User>(await _httpClient.GetFromJsonAsync<User[]>($"{AppConfig.ApiBaseUrl}/users"));
            AuditLogs = new ObservableCollection<AuditLog>(await _httpClient.GetFromJsonAsync<AuditLog[]>($"{AppConfig.ApiBaseUrl}/audit"));
            ErrorLogs = new ObservableCollection<ErrorLog>(await _httpClient.GetFromJsonAsync<ErrorLog[]>($"{AppConfig.ApiBaseUrl}/errors"));
            OnPropertyChanged(nameof(Users));
            OnPropertyChanged(nameof(AuditLogs));
            OnPropertyChanged(nameof(ErrorLogs));
        }

        private async void AddUser()
        {
            var newUser = new User { Username = "Nowy Użytkownik", Email = "nowy@example.com", Role = "User" };
            await _httpClient.PostAsJsonAsync($"{AppConfig.ApiBaseUrl}/users", newUser);
            await LoadData();
        }

        private async void EditUser()
        {
            if (SelectedUser != null)
            {
                await _httpClient.PutAsJsonAsync($"{AppConfig.ApiBaseUrl}/users/{SelectedUser.Id}", SelectedUser);
                await LoadData();
            }
        }

        private async void DeleteUser()
        {
            if (SelectedUser != null)
            {
                await _httpClient.DeleteAsync($"{AppConfig.ApiBaseUrl}/users/{SelectedUser.Id}");
                await LoadData();
            }
        }

        private void SaveConnectionString()
        {
            string configPath = "config.json";
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                JObject config = JObject.Parse(json);
                config["ConnectionString"] = ConnectionString;
                File.WriteAllText(configPath, config.ToString());
            }
        }
    }
}
