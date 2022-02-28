using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAdminService
    {
        Task<ActionResult> GetUsersWithRolesAsync();
        Task<ActionResult> EditRolesAsync(string username, [FromQuery] string roles);
        ActionResult GetPhotosForModeration();
    }
}