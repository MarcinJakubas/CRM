using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using CRM_WPF.Helpers;
using CRM_WPF.Models;

namespace CRM_WPF.Services
{
    public static class ErrorLogger
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task LogError(Exception ex)
        {
            var method = new StackTrace().GetFrame(1)?.GetMethod();
            string methodName = method?.Name ?? "Unknown";
            string className = method?.DeclaringType?.Name ?? "Unknown";

            var error = new
            {
                UserId = UserSession.CurrentUser?.Id,
                Username = UserSession.CurrentUser?.Username,
                ErrorTime = DateTime.UtcNow,
                MethodName = methodName,
                ClassName = className,
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace
            };

            await _httpClient.PostAsJsonAsync($"{AppConfig.ApiBaseUrl}/errors", error);
        }
    }
}
