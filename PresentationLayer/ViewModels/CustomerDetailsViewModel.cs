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
        private readonly DeleteCustomerViewModel _deleteCustomerViewModel;

        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }

        public RelayCommand ShowUpdateCustomerWindow { get; set; }
        public RelayCommand ShowDeleteCustomerWindow { get; set; }


        public UpdateCustomerPopupView updateCustomerWin;

        public DeleteCustomerPopupView deleteCustomerWin;

        public CustomerDetailsViewModel(UpdateCustomerViewModel updateCustomerViewModels, 
            DeleteCustomerViewModel deleteCustomerViewModel) 
        {
            _updateCustomerViewModel = updateCustomerViewModels;
            _deleteCustomerViewModel = deleteCustomerViewModel;
            ShowUpdateCustomerWindow = new RelayCommand(async o => await ShowUpdateWindow(o), o => true);
            ShowDeleteCustomerWindow = new RelayCommand(ShowDeleteWindow, o => true);
        }

        private async Task ShowUpdateWindow(object customerId)
        {
            if (customerId != null)
            {
                await _updateCustomerViewModel.LoadCustomerDetail((int)customerId);
                updateCustomerWin = new UpdateCustomerPopupView(_updateCustomerViewModel);
                updateCustomerWin.Show();
            }
            else
            {
                MessageBox.Show("Customer ID not found");
            }
        }

        private void ShowDeleteWindow(object customerId)
        {
            if (customerId != null)
            {
                _deleteCustomerViewModel.CustomerId = (int) customerId;
                deleteCustomerWin = new DeleteCustomerPopupView(_deleteCustomerViewModel);
                deleteCustomerWin.Show();
            }
            else
            {
                MessageBox.Show("Customer ID not found");
            }
        }
    }
}
