using BusinessServiceLayer;
using BusinessServiceLayer.DTOs;
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

    public MainWindow(IHotelService hotelService)
    {
        InitializeComponent();
        _hotelService = hotelService;
    }

    private void getData_Click(object sender, RoutedEventArgs e)
    {
        IReadOnlyList<RoomTypeDTO> types = _hotelService.GetRoomTypes();
        dgTypeList.ItemsSource = types;
    }
}