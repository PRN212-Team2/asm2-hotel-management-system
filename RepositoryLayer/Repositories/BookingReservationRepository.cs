using RepositoryLayer.Data;
using RepositoryLayer.Models;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLayer.Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<BookingReservation>> GetBookingReservationsByCustomerIdAsync(int customerId)
        {
            string sql = "SELECT * FROM BookingReservation " +
                         "WHERE CustomerID = @CustomerID " +
                         "ORDER BY BookingDate DESC";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerID", customerId);

            List<BookingReservation> bookingReservations = new List<BookingReservation>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookingReservations.Add(new BookingReservation()
                        {
                            BookingReservationID = reader.GetInt32("BookingReservationId"),
                            BookingDate = reader.GetDateTime("BookingDate"),
                            TotalPrice = reader.GetDecimal("TotalPrice"),
                            BookingStatus = reader.GetByte("BookingStatus") != 0,
                            CustomerID = reader.GetInt32("CustomerID")
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
            return bookingReservations;

        }

        public async Task<IReadOnlyList<BookingReservation>> GetBookingReservationsAsync()
        {
            string sql = "SELECT * FROM BookingReservation " +
                         "ORDER BY BookingDate DESC";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            List<BookingReservation> bookingReservations = new List<BookingReservation>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookingReservations.Add(new BookingReservation()
                        {
                            BookingReservationID = reader.GetInt32("BookingReservationId"),
                            BookingDate = reader.GetDateTime("BookingDate"),
                            TotalPrice = reader.GetDecimal("TotalPrice"),
                            BookingStatus = reader.GetByte("BookingStatus") != 0,
                            CustomerID = reader.GetInt32("CustomerID")
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
            return bookingReservations;

        }

        public async Task<IReadOnlyList<BookingReservation>> GetFilteredBookingReservationsAsync(DateTime StartDate, DateTime EndDate)
        {
            string sql = "SELECT * FROM BookingReservation " +
                         "WHERE BookingDate >= @StartDate " +
                         "AND BookingDate <= @EndDate " +
                         "ORDER BY BookingDate DESC;";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@EndDate", EndDate);

            List<BookingReservation> bookingReservations = new List<BookingReservation>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookingReservations.Add(new BookingReservation()
                        {
                            BookingReservationID = reader.GetInt32("BookingReservationId"),
                            BookingDate = reader.GetDateTime("BookingDate"),
                            TotalPrice = reader.GetDecimal("TotalPrice"),
                            BookingStatus = reader.GetByte("BookingStatus") != 0,
                            CustomerID = reader.GetInt32("CustomerID")
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
            return bookingReservations;

        }
    }
}
