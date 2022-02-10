using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>                 //Asta e .Net Identity, pentru o configurare mai usoara si sigura a Authentication
    {
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
        public ICollection<AppUserRole> UserRoles { get; set; }     //Asta e .NET Identity
    }
}