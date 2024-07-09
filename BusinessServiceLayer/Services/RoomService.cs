using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomInformationRepository _roomInformationRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomInformationRepository roomInformationRepository, 
            IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomInformationRepository = roomInformationRepository;
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomInformationDTO>> GetRoomsWithTypeAsync()
        {
            var rooms = await _roomInformationRepository.GetRoomsWithTypeAsync();
            return _mapper.Map<IReadOnlyList<RoomInformation>, IReadOnlyList<RoomInformationDTO>>(rooms);
        }

        public async Task<RoomInformationDTO> GetRoomByIdWithTypeAsync(int id)
        {
            var room = await _roomInformationRepository.GetRoomByIdWithTypeAsync(id);
            if (room == null) return null;
            return _mapper.Map<RoomInformation, RoomInformationDTO>(room);
        }

        public async Task CreateRoomAsync(RoomInformationToAddOrUpdateDTO room)
        {
            if (room == null) throw new ArgumentNullException(nameof(RoomInformationToAddOrUpdateDTO));
            RoomInformation roomToAdd = _mapper.Map<RoomInformationToAddOrUpdateDTO, RoomInformation>(room);
            await _roomInformationRepository.CreateRoomInformationAsync(roomToAdd);
        }

        public async Task DeleteRoomAsync(int id)
        {
            if (_roomInformationRepository.GetRoomInformationByIdForManageAsync(id) == null) throw new ArgumentNullException($"Room {id} not found");
            await _roomInformationRepository.DeleteRoomInformationAsync(id);
        }
        public async Task UpdateRoomAsync(RoomInformationToAddOrUpdateDTO updatedRoom, int id)
        {
            RoomInformation existingRoom = await _roomInformationRepository.GetRoomInformationByIdForManageAsync(id);
            if (existingRoom == null) throw new ArgumentNullException($"Room {id} not found");

            // Update fields only if the new data is not blank or null
            if (!string.IsNullOrWhiteSpace(updatedRoom.RoomNumber))
            {
                existingRoom.RoomNumber = updatedRoom.RoomNumber;
            }

            if (!string.IsNullOrWhiteSpace(updatedRoom.RoomDetailDescription))
            {
                existingRoom.RoomDetailDescription = updatedRoom.RoomDetailDescription;
            }

            if (updatedRoom.RoomMaxCapacity != 0)
            {
                existingRoom.RoomMaxCapacity = updatedRoom.RoomMaxCapacity;
            }

            if (updatedRoom.RoomTypeID != 0)
            {
                existingRoom.RoomTypeID = updatedRoom.RoomTypeID;
            }

            existingRoom.RoomStatus = updatedRoom.RoomStatus;

            if (updatedRoom.RoomPricePerDay != 0)
            {
                existingRoom.RoomPricePerDay = updatedRoom.RoomPricePerDay;
            }

            await _roomInformationRepository.UpdateRoomInformationAsync(existingRoom);
        }
        public async Task<IReadOnlyList<RoomTypeDTO>> GetRoomTypesAsync()
        {
            var roomTypes = await _roomTypeRepository.GetRoomTypesAsync();
            return _mapper.Map<IReadOnlyList<RoomType>, IReadOnlyList<RoomTypeDTO>>(roomTypes);
        }

        public async Task<IReadOnlyList<RoomInformationDTO>> GetRoomInformationForManageAsync()
        {
            var rooms = await _roomInformationRepository.GetRoomInformationForManageAsync();
            return _mapper.Map<IReadOnlyList<RoomInformation>, IReadOnlyList<RoomInformationDTO>>(rooms);
        }

        public async Task<RoomInformationDTO> GetRoomInformationByIdForManageAsync(int id)
        {
            var room = await _roomInformationRepository.GetRoomInformationByIdForManageAsync(id);
            if (room == null) return null;
            return _mapper.Map<RoomInformation, RoomInformationDTO>(room);
        }
    }
}
