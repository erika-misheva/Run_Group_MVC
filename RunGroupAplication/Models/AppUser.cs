using Microsoft.AspNetCore.Identity;

namespace RunGroupAplication.Models
{
    public class AppUser : IdentityUser
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Info { get; set; }
        public string? FirstName {  get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; } 
    }
}
