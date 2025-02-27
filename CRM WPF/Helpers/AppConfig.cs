using Newtonsoft.Json.Linq;
using System.IO;

namespace CRM_WPF.Helpers
{
    public static class AppConfig
    {
        private static readonly JObject _config;

        static AppConfig()
        {
            var json = File.ReadAllText("appsettings.json");
            _config = JObject.Parse(json);
        }

        public static string ApiBaseUrl => _config["ApiSettings"]["BaseUrl"].ToString();
    }
}