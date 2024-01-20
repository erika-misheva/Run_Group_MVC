using Microsoft.AspNetCore.Mvc;

namespace RunGroupAplication.Controllers;

using Data;
using Interfaces;
using Models;
using RunningForce.ViewModels;
using System.Diagnostics;
using ViewModels;

public class ClubController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ClubController(ApplicationDbContext dbContext, IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
        _context = dbContext;
        _clubRepository = clubRepository;
        _photoService = photoService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Club> clubs = await _clubRepository.GetAllClubs();
        return View(clubs);
    }

    public async Task<IActionResult> Detail(int id)
    {
        try
        {
            Club club = await _clubRepository.GetByIdAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet]
    public IActionResult CreateClub()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var createClubVM = new CreateClubViewModel()
        {
            AppUserId = curUserId
        };
        return View(createClubVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> CreateClubPost(CreateClubViewModel clubVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    ShortDescription = clubVM.ShortDescription,
                    LongDescription = clubVM.LongDescription,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street,
                    }
                };

                _clubRepository.AddClub(club);

                return RedirectToAction("Index");
            }
            return View(clubVM);
        }
        catch (Exception ex)
        {

            ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");

            return RedirectToAction("CreateClub", "Club");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null) return View("Error");

        var clubVM = new EditClubViewModel
        {
            Title = club.Title,
            ShortDescription = club.ShortDescription,
            LongDescription = club.LongDescription,
            AddressId = club.AddressId,
            Address = club.Address,
            URL = club.Image,
            ClubCategory = club.ClubCategory,
            AppUserId = club.AppUserId

        };
        return View(clubVM);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(int id, EditClubViewModel editClubViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Faild to edit the Club");
            return View("Edit", editClubViewModel);
        }

        var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

        if (userClub != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userClub.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete the photo");
                return View("Edit", editClubViewModel);
            }
            var photoResult = await _photoService.AddPhotoAsync(editClubViewModel.Image);

            var club = new Club
            {
                Id = id,
                Title = editClubViewModel.Title,
                ShortDescription = editClubViewModel.ShortDescription,
                LongDescription = editClubViewModel.LongDescription,
                AddressId = editClubViewModel.AddressId,
                Address = editClubViewModel.Address,
                Image = photoResult.Url.ToString(),
                ClubCategory = editClubViewModel.ClubCategory,
                AppUserId = editClubViewModel.AppUserId
            };

            _clubRepository.UpdateClub(club);

            return RedirectToAction("Index");
        }
        else
        {
            return View("Edit", editClubViewModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var clubDetails = await _clubRepository.GetByIdAsync(id);
        if (clubDetails == null) return View("Error");
        return View(clubDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var clubDetails = await _clubRepository.GetByIdAsync(id);

        if (clubDetails == null)
        {
            return View("Error");
        }

        if (!string.IsNullOrEmpty(clubDetails.Image))
        {
            _ = _photoService.DeletePhotoAsync(clubDetails.Image);
        }

        _clubRepository.DeleteClub(clubDetails);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddMemberToClub(int id)
    {
        bool addedMember = await _clubRepository.AddMemberToClub(id);

        if (addedMember)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        var errorModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ShowRequestId = false,
            ErrorMessage = "An error occurred while joining. If you are already a member, you can't join the club again. Please check your club list to see if you are already a member.",
            Exception = null
        };

        return View("Error", errorModel);
    }
}


