using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Models;

namespace TSM.Services
{
    public interface IAuthService
    {
        Task CreateUser(UserDto user);
        Task<Jwt> Login(string email, string password);
    }
}
