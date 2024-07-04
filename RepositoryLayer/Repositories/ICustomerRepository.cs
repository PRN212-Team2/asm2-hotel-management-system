using RepositoryLayer.Models;


namespace RepositoryLayer.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Login(string email, string password);
        Task<IReadOnlyList<Customer>> GetCustomersAsync();
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
