using CRM_WPF.Models;
using System;
using System.IO;

namespace CRM_WPF.Services
{
    public static class UserActivityLogger
    {
        private static readonly string LogPath = "user_activity.log";

        public static void Log(string action)
        {
            var user = UserSession.CurrentUser?.Username ?? "Unknown";
            var log = $"{DateTime.Now}: {user} - {action}";
            File.AppendAllText(LogPath, log + Environment.NewLine);
        }
    }
}