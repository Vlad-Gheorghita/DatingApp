using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        IEnumerable<Photo> GetUnapprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void RemovePhoto(int id);
    }
}