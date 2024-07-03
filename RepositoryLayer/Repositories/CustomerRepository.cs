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

        public IReadOnlyList<Customer> GetCustomers()
        {
            string sql = "select * from Customer";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            List<Customer> Customers = new List<Customer>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customers.Add(new Customer()
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
            return Customers;
        }

        public void CreateCustomer(Customer customer)
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
                connection.Open();
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

        public void DeleteCustomer(int id)
        {
            string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
            string reseedSql = "DBCC CHECKIDENT ('Customer', RESEED, 1)";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlCommand reseedCommand = new SqlCommand(reseedSql, connection);

            command.Parameters.AddWithValue("@CustomerId", id);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                reseedCommand.ExecuteNonQuery();
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

        public void UpdateCustomer(Customer updatedCustomer)
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
                connection.Open();
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
        public Customer GetCustomerById(int id)
        {
            string sql = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerId", id);
            Customer customer = null;

            try
            {
                connection.Open();
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

