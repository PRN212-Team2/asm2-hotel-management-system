using RepositoryLayer.Models;

namespace RepositoryLayer.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<IReadOnlyList<RoomType>> GetRoomTypes();
    }
}
