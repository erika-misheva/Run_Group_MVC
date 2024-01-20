using System.ComponentModel.DataAnnotations;

namespace RunGroupAplication.ViewModels;

public class RegisterViewModel
{


    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email Address is required")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "You need to confirm your password")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; }
}
