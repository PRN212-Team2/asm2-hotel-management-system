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
    /// Interaction logic for CreateCustomerPopup.xaml
    /// </summary>
    public partial class CreateCustomerPopup : Window
    {
        private readonly ManageCustomer _manageCustomer;
        private readonly ICustomerService _customerService;
        public CreateCustomerPopup(ICustomerService customerService, ManageCustomer manageCustomer)
        {
            InitializeComponent();
            _customerService = customerService;
            _manageCustomer = manageCustomer;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCustomerFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                    string.IsNullOrWhiteSpace(txtEmailAddress.Text) ||
                    !dpCustomerBirthday.SelectedDate.HasValue ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                CustomerDTO customer = new CustomerDTO
                {
                    CustomerFullName = txtCustomerFullName.Text,
                    Telephone = txtTelephone.Text,
                    EmailAddress = txtEmailAddress.Text,
                    CustomerBirthday = dpCustomerBirthday.SelectedDate.Value,
                    CustomerStatus = chkCustomerStatus.IsChecked ?? false,
                    Password = txtPassword.Text
                };
                _customerService.CreateCustomer(customer);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when creating customer.");
            }
            finally
            {
                _manageCustomer.getCustomers();
            }
        }
    }
}
