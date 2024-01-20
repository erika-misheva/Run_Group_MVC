using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RunGroupAplication.Models;

using Enum;

public class Club
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string ? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string? Image { get; set; }
    [ForeignKey("Address")]
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public ClubCategory ClubCategory { get; set; }
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUserCreator { get; set; }

    public ICollection<AppUser> Members { get; set; } = new List<AppUser>();
}
