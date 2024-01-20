using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace RunGroupAplication.Controllers;


using Data;
using Models;
using ViewModels;
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View(response);
    }

    [HttpPost]

    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);


        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Race");
                }
            }
            TempData["Message"] = "Wrong password. Please try again";

            return View(loginVM);
        }

        TempData["Message"] = "User not found. Please try again";

        return View(loginVM);
    }

    [HttpGet]
    public IActionResult Register()
    {
        var response = new RegisterViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerViewModel);
        }
        var newUser = new AppUser()
        {
            Email = registerViewModel.EmailAddress,
            UserName = registerViewModel.EmailAddress,
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
        if (newUserResponse.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        }
        var userToLogin = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
        var result = await _signInManager.PasswordSignInAsync(userToLogin, registerViewModel.Password, false, false);

        if (result.Succeeded)
        {
            return RedirectToAction("EditUserProfile", "Dashboard");
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Race");
    }

}
