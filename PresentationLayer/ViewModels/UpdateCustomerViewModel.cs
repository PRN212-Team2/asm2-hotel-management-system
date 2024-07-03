using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class UpdateCustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public RelayCommand UpdateCustomerCommand { get; set; }

        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }
        public string Password { get; set; }

        public UpdateCustomerViewModel(ICustomerService customerService, IMapper mapper) 
        {
            _customerService = customerService;
            _mapper = mapper;
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer, o => true);
        }

        public void LoadCustomerDetail(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            CustomerId = customer.CustomerId;
            CustomerFullName = customer.CustomerFullName;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            CustomerBirthday = customer.CustomerBirthday;
            CustomerStatus = customer.CustomerStatus;
        }

        private void UpdateCustomer(object obj)
        {
            if (
                string.IsNullOrWhiteSpace(CustomerFullName) ||
                string.IsNullOrWhiteSpace(Telephone) ||
                string.IsNullOrWhiteSpace(EmailAddress) ||
                !DateTime.TryParse(CustomerBirthday.ToString(), out DateTime parsedBirthday) ||
                string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var customerToUpdate = _mapper.Map<UpdateCustomerViewModel, CustomerToAddOrUpdateDTO>(this);
            _customerService.UpdateCustomer(customerToUpdate, CustomerId);
        }


    }
}
