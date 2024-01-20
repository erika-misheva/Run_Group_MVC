namespace RunGroupAplication.ViewModels;

using Models;
public class UserDetailViewModel
{
    public string Id { get; set; }
    public string UserName { get; set; }

    public int? Pace { get; set; }

    public int? Mileage { get; set; }

    public string ProfileImageUrl { get; set; }

    public string Info { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }

    public List<Race> Races { get; set; }
    public List<Club> Clubs { get; set; }
}
