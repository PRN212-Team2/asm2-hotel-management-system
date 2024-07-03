using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class CustomerDetailsViewModel : ViewModelBase
    {
        private readonly UpdateCustomerViewModel _updateCustomerViewModel;

        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }

        public RelayCommand ShowUpdateCustomerWindow { get; set; }


        public CustomerDetailsViewModel(UpdateCustomerViewModel updateCustomerViewModel) 
        {
            _updateCustomerViewModel = updateCustomerViewModel;
            ShowUpdateCustomerWindow = new RelayCommand(ShowUpdateWindow, o => true);

        }

        private void ShowUpdateWindow(object obj)
        {

            if (obj != null)
            {
                _updateCustomerViewModel.LoadCustomerDetail((int)obj);
                UpdateCustomerPopupView updateCustomerWin = new UpdateCustomerPopupView(_updateCustomerViewModel);
                updateCustomerWin.Show();
            }
            else
            {
                MessageBox.Show("Customer ID not found");
            }

        }
    }
}
