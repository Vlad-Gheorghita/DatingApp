using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ActionResult<UserDto>> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)  // return Unauthorized("Invalid username");
                return new UnauthorizedObjectResult("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) //return  Unauthorized();
                return new UnauthorizedObjectResult("Something went wrong");


            // using var hmac = new HMACSHA512(user.PasswordSalt);

            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // for (int i = 0; i < computedHash.Length; i++)
            // {
            //     if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            // }        <-- Asta e inainte sa folosim .NET Identity

            return new OkObjectResult(new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            });
        }

        public async Task<ActionResult<UserDto>> RegisterUserAsync(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) //return BadRequest("Username is taken");
                return new BadRequestObjectResult("Username is already taken");

            var user = _mapper.Map<AppUser>(registerDto);

            //using var hmac = new HMACSHA512();    //using este folosit ca, clasa HMACSHA512 sa fie disposed(stearsa) corect
            //<-- asta e inainte sa folosim .NET Identity

            user.UserName = registerDto.UserName.ToLower();
            // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            // user.PasswordSalt = hmac.Key;    <-- Asta e inainte sa folosim .NET Identity


            // _context.Users.Add(user); //Add adauga user la urmarirea acestuia in EntityFramework
            // await _context.SaveChangesAsync();  //Acesta apeleaza baza de date pentru a salva user-ul nou

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) //return BadRequest(result.Errors);
                return new BadRequestObjectResult(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) //return BadRequest(result.Errors);
                return new BadRequestObjectResult(roleResult.Errors);


            return new OkObjectResult(new UserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            });
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}