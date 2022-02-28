using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> EditRolesAsync(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return new NotFoundObjectResult("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return new BadRequestObjectResult("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return new BadRequestObjectResult("Failed to remove from roles");

            return new OkObjectResult(await _userManager.GetRolesAsync(user));
        }

        public ActionResult GetPhotosForModeration()
        {
            return new OkObjectResult("Admins or Moderators can see this");
        }

        public async Task<ActionResult> GetUsersWithRolesAsync()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return new OkObjectResult(users);
        }
    }
}