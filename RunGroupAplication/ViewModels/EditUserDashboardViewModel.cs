namespace RunGroupAplication.ViewModels;

public class EditUserDashboardViewModel
{
    public string Id { get; set; }
    public int? Pace {  get; set; }
    public int? Mileage { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public IFormFile Image { get; set; }
    public string Info { get; set; }
    public string? FirstName {  get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
}

