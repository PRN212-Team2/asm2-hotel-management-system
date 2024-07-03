using BusinessServiceLayer.DTOs;

namespace BusinessServiceLayer.Services
{
    public interface IRoomTypeService
    {
        IReadOnlyList<RoomTypeDTO> GetRoomTypes();
    }
}
