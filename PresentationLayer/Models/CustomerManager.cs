using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class CustomerManager
    {
        public ObservableCollection<CustomerDTO> Customers = new ObservableCollection<CustomerDTO>();
        private readonly ICustomerService _customerService;

        public CustomerManager(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            Customers = new ObservableCollection<CustomerDTO>(customers);
        }

        public void AddCustomer(CustomerToAddOrUpdateDTO customerDTO)
        {
            _customerService.CreateCustomer(customerDTO);
            GetCustomers();
        }
    }
}
