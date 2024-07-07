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
    /// <summary>
    /// Interaction logic for CreateCustomerPopup.xaml
    /// </summary>
    public partial class CreateCustomerPopupView : Window
    {
        private readonly CreateCustomerViewModel _createCustomerViewModel;

        public CreateCustomerPopupView(CreateCustomerViewModel createCustomerViewModel)
        {
            InitializeComponent();
            _createCustomerViewModel = createCustomerViewModel;
            this.DataContext = _createCustomerViewModel;
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
