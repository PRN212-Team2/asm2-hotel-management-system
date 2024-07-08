using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using System.Security.Principal;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class CustomerProfileViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }
        public string Password { get; set; } = "";

        public RelayCommand UpdateCustomerCommand { get; set; }

        public CustomerProfileViewModel(IUserService userService, ICustomerService customerService, IMapper mapper)
        {
            _userService = userService;
            _customerService = customerService;
            _mapper = mapper;
            UpdateCustomerCommand = new RelayCommand(async o => await UpdateCustomerAsync(o), CanExecuteUpdateCustomerCommand);
        }

        public async Task loadProfileFromCurrentUser(string email)
        {
            var customer = await _userService.GetUserByEmailAsync(email);
            if(customer != null) 
            {
                CustomerId = customer.Id;
                CustomerFullName = customer.FullName;
                Telephone = customer.Telephone;
                EmailAddress = customer.EmailAddress;
                CustomerBirthday = customer.Birthday;
                CustomerStatus = true;
            }
        }

        private bool CanExecuteUpdateCustomerCommand(object obj)
        {
            if (
                string.IsNullOrWhiteSpace(CustomerFullName) ||
                string.IsNullOrWhiteSpace(Telephone) ||
                string.IsNullOrWhiteSpace(EmailAddress) ||
                !DateTime.TryParse(CustomerBirthday.ToString(), out DateTime parsedBirthday) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            return true;
        }

        public async Task UpdateCustomerAsync(object obj)
        {
            var customerToUpdate = _mapper.Map<CustomerProfileViewModel, CustomerToAddOrUpdateDTO>(this);
            await _customerService.UpdateCustomerAsync(customerToUpdate, CustomerId);
            MessageBox.Show("Profile Updated Successfully");
            
        }
    }
}
