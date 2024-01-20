using Microsoft.AspNetCore.Mvc;

namespace RunGroupAplication.Controllers;

using Interfaces;
using ViewModels;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAppUsers();
        List<UserViewModel> result = new List<UserViewModel>();
        foreach(var user in users)
        {
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                Mileage = user.Mileage,
                UserName = user.UserName,
                Pace = user.Pace,
                ProfileImageUrl = user.ProfileImageUrl
            };
            result.Add(userVM);
        }
        return View(result);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var user = await _userRepository.GetAppUserById(id);
        var userRaces = await _userRepository.GetAllUserRaces(id);
        var userClubs = await _userRepository.GetAllUserClubs(id);
        var userVM = new UserDetailViewModel()
        {
            Id = user.Id,
            Mileage = user.Mileage,
            UserName = user.UserName,
            Pace = user.Pace,
            ProfileImageUrl = user.ProfileImageUrl,
            Info = user.Info,
            Races = userRaces,
            Clubs = userClubs, 
            FirstName = user.FirstName,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber
        };
        return View(userVM);

    }
}
