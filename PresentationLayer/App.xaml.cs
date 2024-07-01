using BusinessServiceLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.Repositories;
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
                services.AddTransient<ICustomerRepository, CustomerRepository>();
                services.AddTransient<ICustomerService, CustomerService>();
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

