using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PhotoRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);
        }

        public IEnumerable<Photo> GetUnapprovedPhotos()
        {
            return _context.Photos.Where(p => p.IsApproved == false).ToList();
            
        }

        public async void RemovePhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            _context.Photos.Remove(photo);
        }
    }
}