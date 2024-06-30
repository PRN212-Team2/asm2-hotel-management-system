using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for DeleteCustomerPopup.xaml
    /// </summary>
    public partial class DeleteCustomerPopup : Window
    {
        private readonly ManageCustomer _manageCustomer;
        private readonly ICustomerService _customerService;
        private readonly int id;
        public DeleteCustomerPopup(ICustomerService customerService, ManageCustomer manageCustomer, int id)
        {
            InitializeComponent();
            _customerService = customerService;
            _manageCustomer = manageCustomer;
            this.id = id;
            DataContext = this;
        }

        public void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when deleting a customer");
            }
            finally
            {
                _manageCustomer.getCustomers();
            }
        }

        public void closeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(id.ToString());
            this.Close();
        }
    }
}
