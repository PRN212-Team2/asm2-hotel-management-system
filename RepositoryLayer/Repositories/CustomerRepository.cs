using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> LoginAsync(string email, string password)
        {
            string sql = "SELECT * FROM Customer " +
                         "WHERE EmailAddress = @CustomerEmail " +
                         "AND Password = @Password " +
                         "AND CustomerStatus = @CustomerStatus";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerEmail", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@CustomerStatus", 1);

            Customer customer = null;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows && reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = reader.GetInt32("CustomerId"),
                        CustomerFullName = reader.GetString("CustomerFullName"),
                        Telephone = reader.GetString("Telephone"),
                        EmailAddress = reader.GetString("EmailAddress"),
                        CustomerBirthday = reader.GetDateTime("CustomerBirthday")
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return customer;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            string sql = "SELECT * FROM Customer " +
                         "WHERE EmailAddress = @CustomerEmail " +
                         "AND CustomerStatus = @CustomerStatus";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerEmail", email);
            command.Parameters.AddWithValue("@CustomerStatus", 1);

            Customer customer = null;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows && reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = reader.GetInt32("CustomerId"),
                        CustomerFullName = reader.GetString("CustomerFullName"),
                        Telephone = reader.GetString("Telephone"),
                        EmailAddress = reader.GetString("EmailAddress"),
                        CustomerBirthday = reader.GetDateTime("CustomerBirthday")
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return customer;
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersAsync()
        {
            string sql = "SELECT * FROM Customer";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            List<Customer> customers = new List<Customer>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerId = reader.GetInt32("CustomerId"),
                            CustomerFullName = reader.GetString("CustomerFullName"),
                            Telephone = reader.GetString("Telephone"),
                            EmailAddress = reader.GetString("EmailAddress"),
                            CustomerBirthday = reader.GetDateTime("CustomerBirthday"),
                            CustomerStatus = reader.GetByte("CustomerStatus") != 0
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return customers;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            string sql = "INSERT INTO Customer (CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) " +
                         "VALUES (@CustomerFullName, @Telephone, @EmailAddress, @CustomerBirthday, @CustomerStatus, @Password);";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName);
            command.Parameters.AddWithValue("@Telephone", customer.Telephone);
            command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
            command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
            command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
            command.Parameters.AddWithValue("@Password", customer.Password);

            try
            {
                await connection.OpenAsync();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            string sql = "UPDATE Customer SET CustomerStatus = @CustomerStatus " +
                         "WHERE CustomerId = @CustomerId";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CustomerStatus", 0);
            command.Parameters.AddWithValue("@CustomerId", id);

            try
            {
                await connection.OpenAsync();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task UpdateCustomerAsync(Customer updatedCustomer)
        {
            string sql = "UPDATE Customer SET CustomerFullName = @CustomerFullName, Telephone = @Telephone, EmailAddress = @EmailAddress, " +
                         "CustomerBirthday = @CustomerBirthday, CustomerStatus = @CustomerStatus, Password = @Password " +
                         "WHERE CustomerId = @CustomerId";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CustomerFullName", updatedCustomer.CustomerFullName);
            command.Parameters.AddWithValue("@Telephone", updatedCustomer.Telephone);
            command.Parameters.AddWithValue("@EmailAddress", updatedCustomer.EmailAddress);
            command.Parameters.AddWithValue("@CustomerBirthday", updatedCustomer.CustomerBirthday);
            command.Parameters.AddWithValue("@CustomerStatus", updatedCustomer.CustomerStatus);
            command.Parameters.AddWithValue("@CustomerId", updatedCustomer.CustomerId);
            command.Parameters.AddWithValue("@Password", updatedCustomer.Password);


            try
            {
                await connection.OpenAsync();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            string sql = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerId", id);
            Customer customer = null;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows && reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = reader.GetInt32("CustomerId"),
                        CustomerFullName = reader.GetString("CustomerFullName"),
                        Telephone = reader.GetString("Telephone"),
                        EmailAddress = reader.GetString("EmailAddress"),
                        CustomerBirthday = reader.GetDateTime("CustomerBirthday"),
                        CustomerStatus = reader.GetByte("CustomerStatus") != 0,
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return customer;
        }
    }
}

