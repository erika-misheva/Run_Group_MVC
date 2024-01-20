using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace RunGroupAplication.Controllers;

using Data;
using Interfaces;
using Models;
using ViewModels;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoService _photoService;

    public DashboardController(ApplicationDbContext context, IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor
        , IPhotoService photoService)
    {
        _context = context;
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoService = photoService;
    }

    public ApplicationDbContext Context { get; }

    public async Task<IActionResult> Index()
    {
        var userRaces = await _dashboardRepository.GetAllUserRaces();
        var userClubs = await _dashboardRepository.GetAllUserClubs();
        var dashboardViewModel = new DashboardViewModel()
        {
            Clubs = userClubs,
            Races = userRaces
        };
        return View(dashboardViewModel);
    }

    public async Task<IActionResult> EditUserProfile()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        AppUser user = await _dashboardRepository.GetAppUserById(curUserId);
        if (user == null) return View("Error");
        var userEditVM = new EditUserDashboardViewModel()
        {
            Id = curUserId,
            Pace = user.Pace,
            Mileage = user.Mileage,
            ProfileImageUrl = user.ProfileImageUrl,
            City = user.City,
            State = user.State,
            Info = user.Info
        };
        return View(userEditVM);
    }

    private void MapUserEdit(AppUser user, EditUserDashboardViewModel editUser, ImageUploadResult imageUploadResult)
    {
        user.Id = editUser.Id;
        user.Mileage = editUser.Mileage;
        user.Pace = editUser.Pace;
        user.ProfileImageUrl = imageUploadResult.Url.ToString();
        user.City = editUser.City;
        user.State = editUser.State;
        user.Info = editUser.Info;
        user.FirstName = editUser.FirstName;
        user.Surname = editUser.Surname;
        user.PhoneNumber = editUser.PhoneNumber;

    }

    [HttpPost]
    public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editUserVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Faild to edit the profile");
            return View("EditUserProfile", editUserVM);
        }
        var user = await _dashboardRepository.GetByIdNoTracking(editUserVM.Id);

        if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
        {
            var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);

            MapUserEdit(user, editUserVM, photoResult);

            _dashboardRepository.Update(user);

            return RedirectToAction("Index", "Dashboard");

        }
        else
        {
            try
            {
                await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Faild while updating photo");
                return View(editUserVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);

            MapUserEdit(user, editUserVM, photoResult);

            _dashboardRepository.Update(user);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
