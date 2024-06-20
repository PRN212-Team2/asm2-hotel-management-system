using RepositoryLayer.Models;

namespace RepositoryLayer
{
    public interface IHotelRepository
    {
        IReadOnlyList<RoomType> GetRoomTypes();
    }
}
