using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;

namespace BusinessServiceLayer.Services
{
    public interface ICustomerService
    {
        IReadOnlyList<CustomerDTO> GetCustomers();

        CustomerDTO GetCustomerById(int id);

        void CreateCustomer(CustomerToAddOrUpdateDTO customer);

        void DeleteCustomer(int id);
        void UpdateCustomer(CustomerToAddOrUpdateDTO updatedCustomer, int id);
    }
}
