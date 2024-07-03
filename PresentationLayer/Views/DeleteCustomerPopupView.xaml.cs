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
    /// Interaction logic for DeleteCustomerPopup.xaml
    /// </summary>
    public partial class DeleteCustomerPopupView : Window
    { 
        private readonly DeleteCustomerViewModel _deleteCustomerViewModel;

        public DeleteCustomerPopupView(DeleteCustomerViewModel deleteCustomerViewModel)
        {
            InitializeComponent();
            _deleteCustomerViewModel = deleteCustomerViewModel;
            DataContext = _deleteCustomerViewModel;
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
