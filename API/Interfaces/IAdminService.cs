using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAdminService
    {
        Task<ActionResult> GetUsersWithRolesAsync();
        Task<ActionResult> EditRolesAsync(string username, [FromQuery] string roles);
        Task<ActionResult> GetPhotosForModeration();
        Task<ActionResult> ApprovePhotoById(int id);
        Task<ActionResult> RejectPhoto(int id);
    }
}