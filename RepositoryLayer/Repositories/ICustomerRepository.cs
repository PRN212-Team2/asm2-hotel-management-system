using RepositoryLayer.Models;


namespace RepositoryLayer.Repositories
{
    public interface ICustomerRepository
    {
        IReadOnlyList<Customer> GetCustomers();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        Customer GetCustomerById(int id);
    }
}
