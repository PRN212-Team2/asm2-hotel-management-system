using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Interfaces
{
    public interface IRoomInformationService
    {
        Task<IReadOnlyList<RoomInformationDTO>> GetRoomsWithTypeAsync();
        Task<RoomInformationDTO> GetRoomByIdWithTypeAsync(int id);
    }
}
