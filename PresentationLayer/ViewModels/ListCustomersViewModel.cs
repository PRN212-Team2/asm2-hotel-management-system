using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.ViewModels
{
    public class ListCustomersViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly CreateCustomerViewModel _createCustomerViewModel;
        private readonly UpdateCustomerViewModel _updateCustomerViewModel;
        private readonly DeleteCustomerViewModel _deleteCustomerViewModel;

        public ObservableCollection<CustomerDetailsViewModel> Customers { get; set; }

        public RelayCommand ShowCreateCustomerWindow {  get; set; }


        public ListCustomersViewModel(ICustomerService customerService, 
            CreateCustomerViewModel createCustomerViewModel,
            UpdateCustomerViewModel updateCustomerViewModel,
            DeleteCustomerViewModel deleteCustomerViewModel)
        {
            _customerService = customerService;
            _createCustomerViewModel = createCustomerViewModel;
            _updateCustomerViewModel = updateCustomerViewModel;
            _deleteCustomerViewModel = deleteCustomerViewModel;
            GetCustomers();
            ShowCreateCustomerWindow = new RelayCommand(ShowCreateWindow, o => true);

            createCustomerViewModel.CustomerCreated += OnCustomerCreated;
        }

        private void ShowCreateWindow(object obj)
        {
            CreateCustomerPopupView createCustomerWin = new CreateCustomerPopupView(_createCustomerViewModel);
            createCustomerWin.Show();
        }

        private void OnCustomerCreated(object sender, EventArgs e)
        {
            // Update Customers list after successful creation
            GetCustomers(); // Refresh the list
        }

        public void GetCustomers()
        {
            var customers = _customerService.GetCustomers();

            var customerObservable = new ObservableCollection<CustomerDetailsViewModel>();
            foreach( var customer in customers ) 
            {
                var customerDetail = new CustomerDetailsViewModel(_updateCustomerViewModel, _deleteCustomerViewModel);
                customerDetail.CustomerId = customer.CustomerId;
                customerDetail.CustomerFullName = customer.CustomerFullName;
                customerDetail.CustomerBirthday = customer.CustomerBirthday;
                customerDetail.CustomerStatus = customer.CustomerStatus;
                customerDetail.Telephone = customer.Telephone;
                customerDetail.EmailAddress = customer.EmailAddress;
                customerObservable.Add(customerDetail);
            }

            Customers = customerObservable;
        }
    }
}
