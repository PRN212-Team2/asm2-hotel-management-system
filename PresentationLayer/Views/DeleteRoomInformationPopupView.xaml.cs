using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for DeleteRoomInformationPopupView.xaml
    /// </summary>
    public partial class DeleteRoomInformationPopupView : Window
    {
        private readonly DeleteRoomInformationViewModel _deleteRoomInformationViewModel;

        public DeleteRoomInformationPopupView(DeleteRoomInformationViewModel deleteRoomInformationViewModel)
        {
            InitializeComponent();
            _deleteRoomInformationViewModel = deleteRoomInformationViewModel;
            DataContext = _deleteRoomInformationViewModel;
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
