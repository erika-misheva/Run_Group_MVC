namespace RunGroupAplication.ViewModels;

using Models;
using Enum;
public class EditRaceViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public IFormFile Image { get; set; }
    public string? URL { get; set; }
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public RaceCategory RaceCategory { get; set; }
    public DateTime StartTime { get; set; }
    public int? EntryFee { get; set; }

    public string AppUserId { get; set; }
}

