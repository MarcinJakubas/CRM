using CRM_WPF.Services;
using CRM_WPF.ViewModels;
using CRM_WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;

namespace CRM_WPF
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<DataService>();

            // Rejestracja ViewModeli
            services.AddTransient<ClientsViewModel>();
            services.AddTransient<TransactionsViewModel>();
            services.AddTransient<ReportsViewModel>();
            services.AddTransient<BaseViewModel>();

            // Rejestracja Views (aby mogły pobierać ViewModel z DI)
            services.AddTransient<ClientsView>();
            services.AddTransient<TransactionsView>();
            services.AddTransient<ReportsView>();
            services.AddTransient<MainWindow>();
        }
    }
}