using RepositoryLayer.Models;
using System.Data.SqlClient;
using System.Data;
using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomInformationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<RoomInformation>> GetRoomsWithTypeAsync()
        {
            string sql = "SELECT a.RoomID, a.RoomNumber, a.RoomDetailDescription, a.RoomMaxCapacity, " +
                "a.RoomPricePerDay, a.RoomTypeID, b.RoomTypeName, b.TypeDescription, b.TypeNote " +
                "FROM RoomInformation a " +
                "LEFT JOIN RoomType b " +
                "ON a.RoomTypeID = b.RoomTypeID " +
                "WHERE a.RoomStatus = 1";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            List<RoomInformation> rooms = new List<RoomInformation>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        rooms.Add(new RoomInformation()
                        {
                            RoomID = reader.GetInt32("RoomID"),
                            RoomNumber = reader.GetString("RoomNumber"),
                            RoomDetailDescription = reader.GetString("RoomDetailDescription"),
                            RoomMaxCapacity = reader.GetInt32("RoomMaxCapacity"),
                            RoomPricePerDay = reader.GetDecimal("RoomPricePerDay"),
                            RoomStatus = true,
                            RoomTypeID = reader.GetInt32("RoomTypeID"),
                            RoomType = new RoomType()
                            {
                                RoomTypeId = reader.GetInt32("RoomTypeID"),
                                RoomTypeName = reader.GetString("RoomTypeName"),
                                TypeDescription = reader.GetString("TypeDescription"),
                                TypeNote = reader.GetString("TypeNote")
                            }
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
            return rooms;
        }

        public async Task<RoomInformation> GetRoomByIdWithTypeAsync(int id)
        {
            string sql = "SELECT a.RoomID, a.RoomNumber, a.RoomDetailDescription, a.RoomMaxCapacity, " +
                         "a.RoomPricePerDay, a.RoomTypeID, b.RoomTypeName, b.TypeDescription, b.TypeNote " +
                         "FROM RoomInformation a " +
                         "LEFT JOIN RoomType b " +
                         "ON a.RoomTypeID = b.RoomTypeID " +
                         "WHERE a.RoomStatus = 1 " +
                         "AND a.RoomID = @RoomID";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RoomID", id);
            RoomInformation room = null;

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                if (reader.HasRows && await reader.ReadAsync())
                {
                    room = new RoomInformation()
                    {
                        RoomID = reader.GetInt32("RoomID"),
                        RoomNumber = reader.GetString("RoomNumber"),
                        RoomDetailDescription = reader.GetString("RoomDetailDescription"),
                        RoomMaxCapacity = reader.GetInt32("RoomMaxCapacity"),
                        RoomPricePerDay = reader.GetDecimal("RoomPricePerDay"),
                        RoomStatus = true,
                        RoomTypeID = reader.GetInt32("RoomTypeID"),
                        RoomType = new RoomType()
                        {
                            RoomTypeId = reader.GetInt32("RoomTypeID"),
                            RoomTypeName = reader.GetString("RoomTypeName"),
                            TypeDescription = reader.GetString("TypeDescription"),
                            TypeNote = reader.GetString("TypeNote")
                        }
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
            return room;
        }
    }
}
