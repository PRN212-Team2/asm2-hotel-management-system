using RepositoryLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<IReadOnlyList<RoomType>> GetRoomTypesAsync();
    }
}
