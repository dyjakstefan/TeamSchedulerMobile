using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;

namespace TSM.Services
{
    public interface IAuthService
    {
        Task CreateUser(UserDto user);
        Task<JwtDto> Login(string email, string password);
    }
}
