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
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AdminService(UserManager<AppUser> userManager, IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
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

        public async Task<ActionResult> GetPhotosForModeration()
        {
            //return new OkObjectResult("Admins or Moderators can see this");

            var photos = _uow.PhotoRepository.GetUnapprovedPhotos();
            List<PhotoForApprovalDto> photosForApproval = new List<PhotoForApprovalDto>();

            foreach (var photo in photos)
            {
                var photoUser = await _uow.UserRepository.GetUserByIdAsync(photo.AppUserId);
                var photoUsername = photoUser.UserName;

                photosForApproval.Add(new PhotoForApprovalDto
                {
                    Id = photo.Id,
                    Url = photo.Url,
                    Username = photoUsername,
                    IsApproved = photo.IsApproved
                });
            }

            return new OkObjectResult(photosForApproval);
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
        public async Task<ActionResult> ApprovePhotoById(int id)
        {
            var photo = await _uow.PhotoRepository.GetPhotoById(id);

            if (photo.IsApproved) return new BadRequestObjectResult("Photo is already approved");

            photo.IsApproved = true;

            if (!photo.AppUser.Photos.Where(p => p.IsMain = true).Any()) photo.IsMain = true;

            if (await _uow.Complete()) return new OkObjectResult(new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url,
                isMain = photo.IsMain,
                IsAppropved = photo.IsApproved
            });

            return new BadRequestObjectResult("Something went wrong");
        }

        public async Task<ActionResult> RejectPhoto(int id)
        {
            _uow.PhotoRepository.RemovePhoto(id);

            if (await _uow.Complete()) return new OkObjectResult("Photo Rejected");

            return new BadRequestObjectResult("Something went wrong");

        }
    }
}