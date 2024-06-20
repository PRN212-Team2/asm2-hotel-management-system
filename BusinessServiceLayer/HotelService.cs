using BusinessServiceLayer.DTOs;
using RepositoryLayer;
using RepositoryLayer.Models;

namespace BusinessServiceLayer;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepo;

    public HotelService(IHotelRepository hotelRepo)
    {
        _hotelRepo = hotelRepo;
    }
    public IReadOnlyList<RoomTypeDTO> GetRoomTypes()
    {
        List<RoomTypeDTO> roomTypes = new List<RoomTypeDTO>();

        foreach(RoomType type in _hotelRepo.GetRoomTypes())
        {
            RoomTypeDTO roomType = new RoomTypeDTO() 
            {
                RoomTypeId = type.RoomTypeId,
                RoomTypeName = type.RoomTypeName,
                TypeDescription = type.TypeDescription,
                TypeNote = type.TypeNote,
            };
            roomTypes.Add(roomType);
        }

        return roomTypes;
    }
}

