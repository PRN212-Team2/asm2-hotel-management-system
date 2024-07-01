using BusinessServiceLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PresentationLayer.Services;
using PresentationLayer.View;
using PresentationLayer.ViewModel;
using RepositoryLayer;
using RepositoryLayer.Models;
using System.Windows;

namespace PresentationLayer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IConfiguration Config { get; private set; }
    public static IHost AppHost { get; private set; }

    public App()
    {
        // Reads appsettings.json file
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Sets up AppHost
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                
                services.AddSingleton<MainWindow>();
                services.AddTransient(sp => new ApplicationDbContext(Config.GetConnectionString("DefaultConnection")));
                services.AddTransient<IHotelRepository, HotelRepository>();
                services.AddTransient<IHotelService, HotelService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModelType => 
                    (ViewModelBase) provider.GetRequiredService(viewModelType));
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<ExampleViewModel>();
                services.AddSingleton<Func<Type, ViewModelBase>>(services => viewModelType 
                => (ViewModelBase) services.GetRequiredService(viewModelType));
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs eventArgs)
    {
        await AppHost!.StartAsync();
        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(eventArgs);
    }

    protected override async void OnExit(ExitEventArgs eventArgs)
    {
        await AppHost!.StopAsync();
        base.OnExit(eventArgs);
    }
}

