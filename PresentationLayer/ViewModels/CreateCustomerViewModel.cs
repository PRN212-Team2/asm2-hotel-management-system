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
    public class CreateCustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public event EventHandler CustomerCreated;
        public RelayCommand CreateCustomerCommand { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public string Password { get; set; }

        public CreateCustomerViewModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
            CreateCustomerCommand = new RelayCommand(async o => await CreateCustomerAsync(o), o => true);
        }

        private async Task CreateCustomerAsync(object obj)
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
            var customerToCreate = _mapper.Map<CreateCustomerViewModel, CustomerToAddOrUpdateDTO>(this);
            await _customerService.CreateCustomerAsync(customerToCreate);
            CustomerCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
