using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System.Security.Principal;

namespace BusinessServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UserService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _customerRepository.GetCustomerByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<Customer, UserDTO>(user);
        }

        public async Task<UserDTO> LoginAsync(string email, string password)
        {
            var user = await _customerRepository.LoginAsync(email, password);
            if(user == null)
            {
                return null;
            }
            return _mapper.Map<Customer, UserDTO>(user);
        }
    }
}
