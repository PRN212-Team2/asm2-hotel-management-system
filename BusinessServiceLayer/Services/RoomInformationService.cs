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
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository _roomInformationRepository;
        private readonly IMapper _mapper;

        public RoomInformationService(IRoomInformationRepository roomInformationRepository, IMapper mapper)
        {
            _roomInformationRepository = roomInformationRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomInformationDTO>> GetRoomInformationAsync()
        {
            var rooms = await _roomInformationRepository.GetRoomInformationAsync();
            return _mapper.Map<IReadOnlyList<RoomInformation>, IReadOnlyList<RoomInformationDTO>>(rooms);
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

        public async Task<RoomInformationDTO> GetRoomInformationByIdAsync(int id)
        {
            RoomInformation room = await _roomInformationRepository.GetRoomInformationByIdAsync(id);
            if (room == null)
            {
                return null;
            }

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
            if (_roomInformationRepository.GetRoomInformationByIdAsync(id) == null) throw new ArgumentNullException($"Room {id} not found");
            await _roomInformationRepository.DeleteRoomInformationAsync(id);
        }
        public async Task UpdateRoomAsync(RoomInformationToAddOrUpdateDTO updatedRoom, int id)
        {
            RoomInformation existingRoom = await _roomInformationRepository.GetRoomInformationByIdAsync(id);
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
    }
}
