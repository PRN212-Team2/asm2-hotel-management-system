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

        public async Task<IReadOnlyList<RoomTypeDTO>> GetRoomTypesAsync()
        {
            var roomTypes = await _roomTypeRepository.GetRoomTypesAsync();
            return _mapper.Map<IReadOnlyList<RoomType>, IReadOnlyList<RoomTypeDTO>>(roomTypes);
        }
    }
}
