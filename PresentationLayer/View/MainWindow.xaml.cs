using BusinessServiceLayer;
using BusinessServiceLayer.DTOs;
using PresentationLayer.ViewModel;
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

namespace PresentationLayer.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IHotelService _hotelService;
    private MainViewModel _mainViewModel;

    public MainWindow(IHotelService hotelService, MainViewModel mainViewModel)
    {
        InitializeComponent();
        _hotelService = hotelService;
        _mainViewModel = mainViewModel;
        DataContext = _mainViewModel;
    }


    private void getData_Click(object sender, RoutedEventArgs e)
    {
        IReadOnlyList<RoomTypeDTO> types = _hotelService.GetRoomTypes();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}