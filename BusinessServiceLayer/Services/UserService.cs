using AutoMapper;
using BusinessServiceLayer.DTOs;
using RepositoryLayer.Models;
using RepositoryLayer.Repositories;
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
        public async Task<UserDTO> Login(string email, string password)
        {
            var user = await _customerRepository.Login(email, password);
            if(user == null)
            {
                return null;
            }
            return _mapper.Map<Customer, UserDTO>(user);
        }
    }
}
