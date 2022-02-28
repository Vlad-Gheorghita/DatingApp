using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAccountService
    {
        Task<ActionResult<UserDto>> RegisterUserAsync(RegisterDto registerDto);
        Task<ActionResult<UserDto>> LoginUserAsync(LoginDto loginDto);
        Task<bool> UserExists(string username);
    }
}