using BusinessServiceLayer.DTOs;

namespace BusinessServiceLayer
{
    public interface IHotelService
    {
        IReadOnlyList<RoomTypeDTO> GetRoomTypes();
    }
}
