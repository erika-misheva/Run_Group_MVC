using System.ComponentModel.DataAnnotations;

namespace RunGroupAplication.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email Address us required")]
    public string EmailAddress { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
