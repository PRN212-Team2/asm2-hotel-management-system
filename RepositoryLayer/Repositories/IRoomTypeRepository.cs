using RepositoryLayer.Models;

namespace RepositoryLayer.Repositories
{
    public interface IRoomTypeRepository
    {
        IReadOnlyList<RoomType> GetRoomTypes();
    }
}
