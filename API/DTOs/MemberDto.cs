using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; } //AutoMapper e destul de smart sa populeze Age cu metoda din AppUser "GetAge" ca are "Get" ca prefix la numele metodei
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }         
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}