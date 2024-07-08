using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLayer.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<RoomType>> GetRoomTypesAsync()
        {
            string sql = "SELECT * FROM RoomType";

            SqlConnection connection = _context.GetConnection();

            SqlCommand command = new SqlCommand(sql, connection);

            List<RoomType> roomTypes = new List<RoomType>();

            try
            {
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        roomTypes.Add(new RoomType()
                        {
                            RoomTypeId = reader.GetInt32("RoomTypeId"),
                            RoomTypeName = reader.GetString("RoomTypeName"),
                            TypeDescription = reader.GetString("TypeDescription"),
                            TypeNote = reader.GetString("TypeNote")
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

            return roomTypes;
        }
    }
}
