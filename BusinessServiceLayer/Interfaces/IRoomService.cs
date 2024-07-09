using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Interfaces
{
    public interface IRoomService
    {
        Task<IReadOnlyList<RoomInformationDTO>> GetRoomsWithTypeAsync();
        Task<RoomInformationDTO> GetRoomByIdWithTypeAsync(int id);
        Task<IReadOnlyList<RoomTypeDTO>> GetRoomTypesAsync();
        Task<IReadOnlyList<RoomInformationDTO>> GetRoomInformationForManageAsync();
        Task<RoomInformationDTO> GetRoomInformationByIdForManageAsync(int id);
        Task CreateRoomAsync(RoomInformationToAddOrUpdateDTO room);
        Task DeleteRoomAsync(int id);
        Task UpdateRoomAsync(RoomInformationToAddOrUpdateDTO updatedRoom, int id);
    }
}
