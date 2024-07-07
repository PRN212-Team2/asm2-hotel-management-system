using AutoMapper;
using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;
using RepositoryLayer.Repositories;
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

    }
}
