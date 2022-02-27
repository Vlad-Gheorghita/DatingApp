using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
  public interface IAccountService
  {
    Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    Task<UserOkDto> LoginUserAsync(LoginDto loginDto);
    Task<bool> UserExists(string username);
  }
}