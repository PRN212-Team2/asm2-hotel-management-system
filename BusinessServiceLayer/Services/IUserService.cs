using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Services
{
    public interface IUserService
    {
        Task<UserDTO> LoginAsync(string email, string password);
        Task<UserDTO> GetUserByEmailAsync(string email);

    }
}
