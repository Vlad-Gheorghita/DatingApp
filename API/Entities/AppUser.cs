using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
        // Am comentat asta deoarece incercam sa nu luam toate datele din baza de date cu scopul optimizarii rularii querry ului -> uita-te in AutoMapperProfiles.cs

        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; }
    }
}