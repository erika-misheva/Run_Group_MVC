using Microsoft.AspNetCore.Mvc;

namespace RunGroupAplication.Controllers;

using Data;
using Interfaces;
using Models;
using ViewModels;
public class RaceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IRaceRepository _raceRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RaceController(ApplicationDbContext context, IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _raceRepository = raceRepository;
        _photoService = photoService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<Race> races = await _raceRepository.GetAllRaces();
        return View(races);
    }

    public async Task<IActionResult> Detail(int id)
    {
        try
        {
            Race race = await _raceRepository.GetByIdAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    [HttpGet]
    public IActionResult CreateRace()
    {
        var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var createRaceVM = new CreateRaceViewModel()
        {
            AppUserId = curUserId
        };
        return View(createRaceVM);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRacePost(CreateRaceViewModel raceVM) { 
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                if (raceVM.StartTime.Date >= DateTime.Today)
                {
                    var race = new Race
                    {
                        Title = raceVM.Title,
                        ShortDescription = raceVM.ShortDescription,
                        LongDescription = raceVM.LongDescription,
                        Image = result.Url.ToString(),
                        RaceCategory = raceVM.RaceCategory,
                        AppUserId = raceVM.AppUserId,
                        Address = new Address
                        {
                            City = raceVM.Address.City,
                            State = raceVM.Address.State,
                            Street = raceVM.Address.Street,
                        },
                        StartTime = raceVM.StartTime,
                        EntryFee = raceVM.EntryFee
                    };
                    _raceRepository.AddRace(race);

                return RedirectToAction("Index");
                } 
            }
            return RedirectToAction("Index", "Race");
        }
        catch (Exception ex)
        {

            ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");

            return RedirectToAction("CreateRace", "Race");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var race = await _raceRepository.GetByIdAsync(id);
        if (race == null) return View("Error");

        var raceVM = new EditRaceViewModel
        {
            Title = race.Title,
            ShortDescription = race.ShortDescription,
            LongDescription = race.LongDescription,
            AddressId = race.AddressId,
            Address = race.Address,
            URL = race.Image,
            RaceCategory = race.RaceCategory,
            EntryFee = race.EntryFee,
            StartTime= race.StartTime,
            AppUserId = _httpContextAccessor.HttpContext.User.GetUserId(),

        };
        return View(raceVM);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(int id, EditRaceViewModel editRaceVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Faild to edit the Club");
            return View("Edit", editRaceVM);
        }

        var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);

        if (userRace != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userRace.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete the photo");
                return View("Edit", editRaceVM);
            }
            var photoResult = await _photoService.AddPhotoAsync(editRaceVM.Image);
            if(editRaceVM.StartTime.Date > DateTime.Today)
            {  
                var race = new Race
            {
                Id = id,
                Title = editRaceVM.Title,
                ShortDescription = editRaceVM.ShortDescription,
                LongDescription = editRaceVM.LongDescription,
                AddressId = editRaceVM.AddressId,
                Address = editRaceVM.Address,
                Image = photoResult.Url.ToString(),
                RaceCategory = editRaceVM.RaceCategory,
                StartTime = editRaceVM.StartTime,
                EntryFee = editRaceVM.EntryFee,
                AppUserId = editRaceVM.AppUserId
            };

            _raceRepository.UpdateRace(race);

            
            }
         return RedirectToAction("Index");
        }
        else
        {
            return View("Edit", editRaceVM);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var raceDetails = await _raceRepository.GetByIdAsync(id);
        if (raceDetails == null) return View("Error");
        return View(raceDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteRace(int id)
    {
        var raceDetails = await _raceRepository.GetByIdAsync(id);

        if (raceDetails == null)
        {
            return View("Error");
        }

        if (!string.IsNullOrEmpty(raceDetails.Image))
        {
            _ = _photoService.DeletePhotoAsync(raceDetails.Image);
        }

        _raceRepository.DeleteRace(raceDetails);
        return RedirectToAction("Index");
    }


}
