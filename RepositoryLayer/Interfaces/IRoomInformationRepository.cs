using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRoomInformationRepository
    {
        Task<IReadOnlyList<RoomInformation>> GetRoomsWithTypeAsync();
        Task<RoomInformation> GetRoomByIdWithTypeAsync(int id);
    }
}
