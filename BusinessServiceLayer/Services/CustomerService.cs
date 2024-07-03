using RepositoryLayer.Repositories;
using RepositoryLayer.Models;
using BusinessServiceLayer.DTOs;
using AutoMapper;


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

        public IReadOnlyList<CustomerDTO> GetCustomers()
        {
            var customers = _customerRepo.GetCustomers();
            return _mapper.Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDTO>>(customers);
        }

        public CustomerDTO GetCustomerById(int id)
        {
            Customer customer = _customerRepo.GetCustomerById(id);
            if(customer == null) 
            {
                return null;
            }
            
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public void CreateCustomer(CustomerToAddOrUpdateDTO customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(CustomerToAddOrUpdateDTO));
            Customer customerToAdd = _mapper.Map<CustomerToAddOrUpdateDTO, Customer>(customer);
            _customerRepo.CreateCustomer(customerToAdd);
        }

        public void DeleteCustomer(int id)
        {
            if (_customerRepo.GetCustomerById(id) == null) throw new ArgumentNullException($"Customer {id} not found");
            _customerRepo.DeleteCustomer(id);
        }
        public void UpdateCustomer(CustomerToAddOrUpdateDTO updatedCustomer, int id)
        {
            Customer existingCustomer = _customerRepo.GetCustomerById(id);
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

            _customerRepo.UpdateCustomer(existingCustomer);
        }   
    }
}

