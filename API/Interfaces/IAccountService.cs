using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IAccountService
    {
        Task<UserOkDto> RegisterUserAsync(RegisterDto registerDto);
        Task<UserOkDto> LoginUserAsync(LoginDto loginDto);
        Task<bool> UserExists(string username);
    }
}