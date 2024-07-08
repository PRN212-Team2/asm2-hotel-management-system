using RepositoryLayer.Models;


namespace RepositoryLayer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> LoginAsync(string email, string password);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<IReadOnlyList<Customer>> GetCustomersAsync();
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
