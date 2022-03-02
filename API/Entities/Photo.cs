using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public AppUser AppUser { get; set; }    //  
        public int AppUserId { get; set; }      //  Asta e relatie de EntityFramework in care Tabelei Photos ii se da o cheie straina pentru un AppUser
        public bool IsApproved { get; set; }
    }
}