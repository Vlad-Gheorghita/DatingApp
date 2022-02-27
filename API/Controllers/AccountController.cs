using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {

    // private readonly ITokenService _tokenService;
    // private readonly IMapper _mapper;
    // private readonly UserManager<AppUser> _userManager;
    // private readonly SignInManager<AppUser> _signInManager;
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)  //UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)   //DataContext context
    {
      _accountService = accountService;
      // _signInManager = signInManager;
      // _userManager = userManager;
      // _mapper = mapper;
      // _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      var user = await _accountService.RegisterUserAsync(registerDto);

      // if(!user.IsOk)
      // {
      //     if(user.Message != null) return BadRequest(user.Message);
      //     return BadRequest(user.Error); 
      // }

      return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _accountService.LoginUserAsync(loginDto);
      if (!user.IsOk) return Unauthorized(user.Message);
      return Ok(user.User);
    }

    // private async Task<bool> UserExists(string username)
    // {
    //     return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    // }
  }
}