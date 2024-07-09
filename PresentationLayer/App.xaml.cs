using BusinessServiceLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.Repositories;
using PresentationLayer.Views;
using PresentationLayer.Services;
using PresentationLayer.ViewModels;
using System.Windows;
using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;
using BusinessServiceLayer.Interfaces;

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
                services.AddSingleton<LoginView>();
                services.AddTransient(sp => new ApplicationDbContext(Config.GetConnectionString("DefaultConnection")));
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModelType => 
                    (ViewModelBase) provider.GetRequiredService(viewModelType));
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<ListCustomersViewModel>();
                services.AddSingleton<ListReportStatisticsViewModel>();
                services.AddSingleton<ManageCustomerView>();
                services.AddSingleton<CreateCustomerViewModel>();
                services.AddSingleton<UpdateCustomerViewModel>();
                services.AddSingleton<DeleteCustomerViewModel>();
                services.AddSingleton<ListBookingReservationHistoryViewModel>();
                services.AddSingleton<CustomerProfileViewModel>();
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<BookingReservationDetailViewModel>();
                services.AddSingleton<MakeReservationViewModel>();
                services.AddSingleton<RoomInformationDetailsViewModel>();
                services.AddSingleton<ListRoomInformationViewModel>();
                services.AddSingleton<CreateRoomInformationViewModel>();
                services.AddSingleton<DeleteRoomInformationViewModel>();
                services.AddSingleton<UpdateRoomInformationViewModel>();
                services.AddSingleton<Func<Type, ViewModelBase>>(services => viewModelType 
                => (ViewModelBase) services.GetRequiredService(viewModelType));
                services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
                services.AddTransient<IBookingReservationRepository, BookingReservationRepository>();
                services.AddTransient<ICustomerRepository, CustomerRepository>();
                services.AddTransient<IRoomInformationRepository, RoomInformationRepository>();
                services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
                services.AddTransient<ICustomerService, CustomerService>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IBookingReservationService, BookingReservationService>();
                services.AddTransient<IRoomService, RoomService>();
                services.AddTransient<IBookingDetailRepository, BookingDetailRepository>();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs eventArgs)
    {
        await AppHost!.StartAsync();
        var loginView = AppHost.Services.GetRequiredService<LoginView>();
        var loginViewModel = AppHost.Services.GetRequiredService<LoginViewModel>();
        var mainViewModel = AppHost.Services.GetService<MainViewModel>();
        loginView.Show();
        loginView.IsVisibleChanged += (s, ev) =>
        {
            if (loginViewModel.IsViewVisible == false && loginView.IsLoaded)
            {
                var mainView = new MainWindow(mainViewModel, loginViewModel);
                mainView.Show();
            }
        };

        base.OnStartup(eventArgs);
    }

    protected override async void OnExit(ExitEventArgs eventArgs)
    {
        await AppHost!.StopAsync();
        base.OnExit(eventArgs);
    }
}

