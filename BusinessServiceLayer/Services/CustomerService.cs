using RepositoryLayer.Models;
using BusinessServiceLayer.DTOs;
using AutoMapper;
using RepositoryLayer.Interfaces;
using BusinessServiceLayer.Interfaces;


namespace BusinessServiceLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _customerRepo.GetCustomersAsync();
            return _mapper.Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            Customer customer = await _customerRepo.GetCustomerByIdAsync(id);
            if(customer == null) 
            {
                return null;
            }
            
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public async Task CreateCustomerAsync(CustomerToAddOrUpdateDTO customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(CustomerToAddOrUpdateDTO));
            Customer customerToAdd = _mapper.Map<CustomerToAddOrUpdateDTO, Customer>(customer);
            await _customerRepo.CreateCustomerAsync(customerToAdd);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (_customerRepo.GetCustomerByIdAsync(id) == null) throw new ArgumentNullException($"Customer {id} not found");
            await _customerRepo.DeleteCustomerAsync(id);
        }
        public async Task UpdateCustomerAsync(CustomerToAddOrUpdateDTO updatedCustomer, int id)
        {
            Customer existingCustomer = await _customerRepo.GetCustomerByIdAsync(id);
            if (existingCustomer == null) throw new ArgumentNullException($"Customer {id} not found");

            // Update fields only if the new data is not blank or null
            if (!string.IsNullOrWhiteSpace(updatedCustomer.CustomerFullName))
            {
                existingCustomer.CustomerFullName = updatedCustomer.CustomerFullName;
            }

            if (!string.IsNullOrWhiteSpace(updatedCustomer.Telephone))
            {
                existingCustomer.Telephone = updatedCustomer.Telephone;
            }

            if (!string.IsNullOrWhiteSpace(updatedCustomer.EmailAddress))
            {
                existingCustomer.EmailAddress = updatedCustomer.EmailAddress;
            }

            if (updatedCustomer.CustomerBirthday != default(DateTime))
            {
                existingCustomer.CustomerBirthday = updatedCustomer.CustomerBirthday;
            }

            existingCustomer.CustomerStatus = updatedCustomer.CustomerStatus;

            if (!string.IsNullOrWhiteSpace(updatedCustomer.Password))
            {
                existingCustomer.Password = updatedCustomer.Password;
            }

            await _customerRepo.UpdateCustomerAsync(existingCustomer);
        }   
    }
}

