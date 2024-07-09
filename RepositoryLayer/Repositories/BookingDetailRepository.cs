using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddBookingDetailAsync(BookingDetail bookingDetail)
        {
            string sql = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) " +
                         "VALUES (@BookingReservationID, @RoomID, @StartDate, @EndDate, @ActualPrice)";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@BookingReservationID", bookingDetail.BookingReservationID);
            command.Parameters.AddWithValue("@RoomID", bookingDetail.RoomID);
            command.Parameters.AddWithValue("@StartDate", bookingDetail.StartDate);
            command.Parameters.AddWithValue("@EndDate", bookingDetail.EndDate);
            command.Parameters.AddWithValue("@ActualPrice", bookingDetail.ActualPrice);

            try
            {
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
