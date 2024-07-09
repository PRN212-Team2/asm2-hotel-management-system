using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
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
    public partial class CreateRoomInformationPopupView : Window
    {
        private readonly CreateRoomInformationViewModel _createRoomInformationViewModel;

        public CreateRoomInformationPopupView(CreateRoomInformationViewModel createRoomInformationViewModel)
        {
            InitializeComponent();
            _createRoomInformationViewModel = createRoomInformationViewModel;
            this.DataContext = _createRoomInformationViewModel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void popupFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
