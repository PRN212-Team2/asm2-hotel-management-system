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
                await connection.CloseAsync();
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
                await connection.CloseAsync();
            }
            return room;
        }

        public async Task CreateRoomInformationAsync(RoomInformation roomInformation)
        {
            string sql = "INSERT INTO RoomInformation (RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay) " +
                         "VALUES (@RoomNumber, @RoomDetailDescription, @RoomMaxCapacity, @RoomTypeID, @RoomStatus, @RoomPricePerDay);";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoomNumber", roomInformation.RoomNumber);
            command.Parameters.AddWithValue("@RoomDetailDescription", roomInformation.RoomDetailDescription);
            command.Parameters.AddWithValue("@RoomMaxCapacity", roomInformation.RoomMaxCapacity);
            command.Parameters.AddWithValue("@RoomTypeID", roomInformation.RoomTypeID);
            command.Parameters.AddWithValue("@RoomStatus", roomInformation.RoomStatus);
            command.Parameters.AddWithValue("@RoomPricePerDay", roomInformation.RoomPricePerDay);

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

        public async Task DeleteRoomInformationAsync(int id)
        {
            string sql = "UPDATE RoomInformation SET RoomStatus = @RoomStatus " +
                         "WHERE RoomID = @RoomID";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoomStatus", 0);
            command.Parameters.AddWithValue("@RoomID", id);

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

        public async Task UpdateRoomInformationAsync(RoomInformation updatedRoomInformation)
        {
            string sql = "UPDATE RoomInformation SET RoomNumber = @RoomNumber, RoomDetailDescription = @RoomDetailDescription, RoomMaxCapacity = @RoomMaxCapacity, " +
                         "RoomTypeID = @RoomTypeID, RoomStatus = @RoomStatus, RoomPricePerDay = @RoomPricePerDay " +
                         "WHERE RoomID = @RoomID";
            SqlConnection connection = _context.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@RoomNumber", updatedRoomInformation.RoomNumber);
            command.Parameters.AddWithValue("@RoomDetailDescription", updatedRoomInformation.RoomDetailDescription);
            command.Parameters.AddWithValue("@RoomMaxCapacity", updatedRoomInformation.RoomMaxCapacity);
            command.Parameters.AddWithValue("@RoomTypeID", updatedRoomInformation.RoomTypeID);
            command.Parameters.AddWithValue("@RoomStatus", updatedRoomInformation.RoomStatus);
            command.Parameters.AddWithValue("@RoomId", updatedRoomInformation.RoomID);
            command.Parameters.AddWithValue("@RoomPricePerDay", updatedRoomInformation.RoomPricePerDay);


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
        public async Task<RoomInformation> GetRoomInformationByIdForManageAsync(int id)
        {
            string sql = "SELECT a.RoomID, a.RoomNumber, a.RoomDetailDescription, a.RoomMaxCapacity, " +
                         "a.RoomPricePerDay, a.RoomTypeID, a.RoomStatus, b.RoomTypeName, b.TypeDescription, b.TypeNote " +
                         "FROM RoomInformation a " +
                         "LEFT JOIN RoomType b " +
                         "ON a.RoomTypeID = b.RoomTypeID " +
                         "WHERE a.RoomID = @RoomID";
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
                        RoomStatus = reader.GetByte("RoomStatus") != 0,
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
                await connection.CloseAsync();
            }
            return room;
        }

        public async Task<IReadOnlyList<RoomInformation>> GetRoomInformationForManageAsync()
        {
            string sql = "SELECT a.RoomID, a.RoomNumber, a.RoomDetailDescription, a.RoomMaxCapacity, " +
                "a.RoomPricePerDay, a.RoomTypeID, a.RoomStatus, b.RoomTypeName, b.TypeDescription, b.TypeNote " +
                "FROM RoomInformation a " +
                "LEFT JOIN RoomType b " +
                "ON a.RoomTypeID = b.RoomTypeID";
                
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
                            RoomStatus = reader.GetByte("RoomStatus") != 0,
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
                await connection.CloseAsync();
            }
            return rooms;
        }
    }
}
