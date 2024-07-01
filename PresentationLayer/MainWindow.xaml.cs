using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using RepositoryLayer.Repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IHotelService _hotelService;
    private readonly ICustomerService _customerService;

    public MainWindow(IHotelService hotelService, ICustomerService customerService)
    {
        InitializeComponent();
        _hotelService = hotelService;
        _customerService = customerService;
    }

    private void getData_Click(object sender, RoutedEventArgs e)
    {
        IReadOnlyList<RoomTypeDTO> types = _hotelService.GetRoomTypes();
        dgTypeList.ItemsSource = types;
    }

    private void manageCustomer_Click(Object sender, RoutedEventArgs e)
    {
        MainContent.Visibility = Visibility.Collapsed;
        MainFrame.Visibility = Visibility.Visible;
        MainFrame.Navigate(new ManageCustomer(_customerService));
    }
}