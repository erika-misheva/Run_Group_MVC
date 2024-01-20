using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RunGroupAplication.Models;

using Enum;
public class Race
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? LongDescription { get; set; }
    public string? Image { get; set; }
    public DateTime StartTime { get; set; }
    public int? EntryFee { get; set; }
    public string? Contact { get; set; }
    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public RaceCategory RaceCategory { get; set; }
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    //public List<AppUser> Participants { get; set; }
}
