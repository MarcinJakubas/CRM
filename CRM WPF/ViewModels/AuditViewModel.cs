using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CRM_WPF.Helpers;
using CRM_WPF.Models;

namespace CRM_WPF.ViewModels
{
    public class AuditViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<AuditLog> AuditLogs { get; set; }

        public AuditViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            AuditLogs = new ObservableCollection<AuditLog>(); 
            _ = LoadAuditLogsAsync();
        }

        private async Task LoadAuditLogsAsync()
        {
            var logs = await _httpClient.GetFromJsonAsync<AuditLog[]>($"{AppConfig.ApiBaseUrl}/audit");
            foreach (var log in logs)
            {
                AuditLogs.Add(log);
            }
        }
    }
}
