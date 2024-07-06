using RepositoryLayer.Data;
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
        public IReadOnlyList<RoomType> GetRoomTypes()
        {
            string sql = "SELECT * FROM RoomType";

            SqlConnection connection = _context.GetConnection();

            SqlCommand command = new SqlCommand(sql, connection);

            List<RoomType> roomTypes = new List<RoomType>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
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
