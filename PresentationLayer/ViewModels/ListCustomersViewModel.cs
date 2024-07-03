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
        private readonly IMapper _mapper;

        public ObservableCollection<CustomerDTO> Customers { get; set; }

        public RelayCommand ShowCreateCustomerWindow {  get; set; }
        public RelayCommand ShowUpdateCustomerWindow { get; set; }


        public ListCustomersViewModel(ICustomerService customerService, 
            CreateCustomerViewModel createCustomerViewModel,
            UpdateCustomerViewModel updateCustomerViewModel, IMapper mapper)
        {
            _customerService = customerService;
            _createCustomerViewModel = createCustomerViewModel;
            _updateCustomerViewModel = updateCustomerViewModel;
            _mapper = mapper;
            GetCustomers();
            ShowCreateCustomerWindow = new RelayCommand(ShowCreateWindow, o => true);
            ShowUpdateCustomerWindow = new RelayCommand(ShowUpdateWindow, o => true);

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

        private void ShowUpdateWindow(object obj)
        {
            
            if(obj != null)
            {
                _updateCustomerViewModel.LoadCustomerDetail((int) obj);
                UpdateCustomerPopupView updateCustomerWin = new UpdateCustomerPopupView(_updateCustomerViewModel);
                updateCustomerWin.Show();
            }
            else
            {
                MessageBox.Show("Customer ID not found");
            }
            
        }

        public void GetCustomers()
        {
            var customers = _customerService.GetCustomers();

            Customers = new ObservableCollection<CustomerDTO>(customers);
        }
    }
}
