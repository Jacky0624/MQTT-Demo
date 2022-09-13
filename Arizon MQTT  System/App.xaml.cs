using Arizon_MQTT__System.Services.Messages;
using Arizon_MQTT__System.Services.Navigations;
using Arizon_MQTT__System.Stores;
using Arizon_MQTT__System.ViewModels;
using Arizon_MQTT__System.ViewModels.Homes;
using Arizon_MQTT__System.ViewModels.Mails;
using Arizon_MQTT_System.API;
using Arizon_MQTT_System.API.Services;
using Arizon_MQTT_System.Domain.Models.Settings;
using Arizon_MQTT_System.Domain.Models.Settings.Mails;
using Arizon_MQTT_System.Domain.Services.Mails;
using Arizon_MQTT_System.Domain.Services.Mqtts.Temperatures;
using Arizon_MQTT_System.Domain.Services.Navigators;
using Arizon_MQTT_System.Domain.Services.Temperatures;
using Arizon_MQTT_System.Repository.SettingObjs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Arizon_MQTT__System
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;


        public App()
        {
            _host = CreateHostBuilder()
                 .ConfigureServices((hostContext, services) =>
                 {

                     services.AddHttpClient<MESHttpClient>(c =>
                     {
                         c.BaseAddress = new Uri("http://10.35.2.22:9530/");
                     });
                     var dbSettings = hostContext.Configuration.GetSection("DBSettings").Get<RepositorySetting>();
                     var mqttConfig = hostContext.Configuration.GetSection("TempMqttConfig").Get<MqttClientConfiguration>();
                     services.AddSingleton<MailConfig>(s => new MailConfig()
                     {
                         DisplayName = "溫度警示",
                         IsBodyHtml = false,
                     });

                     services.AddSingleton<ITemperatureMailService, TemperatureMailService>();
                     services.AddSingleton<SendTemperatureMailStore>();
                     services.AddSingleton<IMessageService, MessageBoxService>();
                     services.AddSingleton<MqttFactory>();
                     services.AddSingleton<IMailService, MailService>();
                     services.AddSingleton<ITemperatureMqttService, TemperatureMqttService>(
                         s => new TemperatureMqttService(
                             s.GetRequiredService<MqttFactory>(),
                             mqttConfig));
                     services.AddSingleton<NavigationStore>();
                     services.AddSingleton<ModalNavigationStore>();
                     services.AddSingleton<CloseModalNavigationService>();
                     services.AddSingleton<HomeViewModel>();

                     services.AddSingleton<SendTemperatureMailViewModel>();
                     services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
                     services.AddScoped<NavigationBarViewModel>(CreateNavigationBarViewModel);

                     services.AddSingleton<MainViewModel>(s => new MainViewModel(
                         s.GetRequiredService<NavigationStore>(),
                         s.GetRequiredService<NavigationBarViewModel>(),
                         s.GetRequiredService<ModalNavigationStore>()));
                     services.AddSingleton<MainWindow>(s => new MainWindow()
                     {
                         DataContext = s.GetRequiredService<MainViewModel>()
                     });
                 })
                 .Build();
        }
        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                serviceProvider.GetRequiredService<NavigationStore>(),
                CreateHomeNavigationService(serviceProvider),
                CreateSendTemperatureNavigationService(serviceProvider)
                );
        }

        private INavigationService CreateSendTemperatureNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<SendTemperatureMailViewModel>(
              serviceProvider.GetRequiredService<NavigationStore>(),
              () => serviceProvider.GetRequiredService<SendTemperatureMailViewModel>());
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeViewModel>(
               serviceProvider.GetRequiredService<NavigationStore>(),
               () => serviceProvider.GetRequiredService<HomeViewModel>());
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();
            _host.Services.GetRequiredService<SendTemperatureMailViewModel>();
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
