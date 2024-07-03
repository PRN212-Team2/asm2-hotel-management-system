using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;
using RepositoryLayer.Repositories;

namespace BusinessServiceLayer.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _hotelRepo;

    public RoomTypeService(IRoomTypeRepository hotelRepo)
    {
        _hotelRepo = hotelRepo;
    }
    public IReadOnlyList<RoomTypeDTO> GetRoomTypes()
    {
        List<RoomTypeDTO> roomTypes = new List<RoomTypeDTO>();

        foreach (RoomType type in _hotelRepo.GetRoomTypes())
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

