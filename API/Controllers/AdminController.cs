using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            return await _adminService.GetUsersWithRolesAsync();
        }

        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            return await _adminService.EditRolesAsync(username, roles);
        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult> GetPhotosForModeration()
        {
            return await _adminService.GetPhotosForModeration();
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("photos-to-moderate/approve/{id}")]
        public async Task<ActionResult<PhotoDto>> ApprovePhotoById(int id)
        {
            return await _adminService.ApprovePhotoById(id);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpDelete("photos-to-moderate/reject/{id}")]
        public async Task<ActionResult> RejectPhotoById(int id)
        {
            return await _adminService.RejectPhoto(id);
        }
    }
}