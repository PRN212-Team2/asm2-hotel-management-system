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

        public async Task<BookingReservation> GetBookingReservationByIdAsync(int id)
        {
            string sql = "SELECT a.BookingReservationID, a.BookingDate, a.TotalPrice, a.CustomerID, a.BookingStatus, b.RoomID, b.StartDate, b.EndDate, b.ActualPrice " +
                "FROM BookingReservation a " +
                "LEFT JOIN BookingDetail b ON a.BookingReservationID = b.BookingReservationID " +
                "WHERE a.BookingReservationID = @BookingReservationID";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@BookingReservationID", id);

            BookingReservation bookingReservation = new BookingReservation();
            List<BookingDetail> bookingDetails = new List<BookingDetail>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    bookingReservation.BookingReservationID = reader.GetInt32("BookingReservationID");
                    bookingReservation.BookingDate = reader.GetDateTime("BookingDate");
                    bookingReservation.TotalPrice = reader.GetDecimal("TotalPrice");
                    bookingReservation.CustomerID = reader.GetInt32("CustomerID");
                    bookingReservation.BookingStatus = reader.GetByte("BookingStatus") != 0;

                    bookingDetails.Add(new BookingDetail()
                    {
                        BookingReservationID = reader.GetInt32("BookingReservationID"),
                        RoomID = reader.GetInt32("RoomID"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                        ActualPrice = reader.GetDecimal("ActualPrice")
                    });
                }
                bookingReservation.BookingDetails = bookingDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return bookingReservation;
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
