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
    /// Interaction logic for UpdateCustomerPopup.xaml
    /// </summary>
    public partial class UpdateCustomerPopup : Window
    {
        private readonly ManageCustomer _manageCustomer;
        private readonly ICustomerService _customerService;
        private readonly int id;
        public UpdateCustomerPopup(ICustomerService customerService, ManageCustomer manageCustomer, int id)
        {
            InitializeComponent();
            _customerService = customerService;
            _manageCustomer = manageCustomer;
            this.id = id;
            getCustomerById(id);
        }

        public void getCustomerById(int id)
        {
            CustomerDTO customer = _customerService.GetCustomerById(id);
            UpdateForm.DataContext = customer;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerDTO updatedCustomer = new CustomerDTO()
                {
                    CustomerFullName = txtNewCustomerFullName.Text,
                    Telephone = txtNewTelephone.Text,
                    EmailAddress = txtNewEmailAddress.Text,
                    CustomerBirthday = dpNewCustomerBirthday.SelectedDate.Value,
                    CustomerStatus = chkNewCustomerStatus.IsChecked ?? false,
                    Password = txtNewPassword.Text,
                };
                _customerService.UpdateCustomer(updatedCustomer, id);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(id.ToString());
            }
            finally
            {
                _manageCustomer.getCustomers();
            }
        }
    }
}
