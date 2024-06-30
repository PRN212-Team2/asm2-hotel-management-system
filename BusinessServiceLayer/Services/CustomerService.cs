using RepositoryLayer.Repositories;
using RepositoryLayer.Models;
using BusinessServiceLayer.DTOs;


namespace BusinessServiceLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public IReadOnlyList<CustomerDTO> GetCustomers()
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();
            foreach (Customer cus in _customerRepo.GetCustomers())
            {
                if (cus != null)
                {
                    CustomerDTO customer = new CustomerDTO()
                    {
                        CustomerId = cus.CustomerId,
                        CustomerFullName = cus.CustomerFullName,
                        Telephone = cus.Telephone,
                        EmailAddress = cus.EmailAddress,
                        CustomerBirthday = cus.CustomerBirthday,
                        CustomerStatus = cus.CustomerStatus,
                        Password = cus.Password,
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public CustomerDTO GetCustomerById(int id)
        {
            Customer cus = _customerRepo.GetCustomerById(id);
            if(cus == null) 
            {
                return null;
            }
            CustomerDTO customer = new CustomerDTO()
            {
                CustomerFullName = cus.CustomerFullName,
                Telephone = cus.Telephone,
                EmailAddress = cus.EmailAddress,
                CustomerBirthday = cus.CustomerBirthday,
                CustomerStatus = cus.CustomerStatus,
                Password = cus.Password,
            };
            return customer;
        }

        public void CreateCustomer(CustomerDTO customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(CustomerDTO));
            Customer cus = new Customer()
            {
                CustomerFullName = customer.CustomerFullName,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerStatus = customer.CustomerStatus,
                Password = customer.Password,
            };
            _customerRepo.CreateCustomer(cus);
        }

        public void DeleteCustomer(int id)
        {
            if (_customerRepo.GetCustomerById(id) == null) throw new ArgumentNullException($"Customer {id} not found");
            _customerRepo.DeleteCustomer(id);
        }
        public void UpdateCustomer(CustomerDTO updatedCustomer, int id)
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

