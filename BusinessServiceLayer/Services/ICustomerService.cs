using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;

namespace BusinessServiceLayer.Services
{
    public interface ICustomerService
    {
        Task<IReadOnlyList<CustomerDTO>> GetCustomersAsync();

        Task<CustomerDTO> GetCustomerByIdAsync(int id);

        Task CreateCustomerAsync(CustomerToAddOrUpdateDTO customer);

        Task DeleteCustomerAsync(int id);
        Task UpdateCustomerAsync(CustomerToAddOrUpdateDTO updatedCustomer, int id);
    }
}
