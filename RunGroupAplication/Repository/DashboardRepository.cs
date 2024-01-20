using Microsoft.EntityFrameworkCore;

namespace RunGroupAplication.Repository;

using Data;
using Interfaces;
using Models;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<Club>> GetAllUserClubs()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userClubs =  _context.Clubs.Where(c => c.AppUserCreator.Id == curUser).ToList();
        return userClubs;
    }

    public async Task<List<Race>> GetAllUserRaces()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userRaces = _context.Races.Where(c => c.AppUser.Id == curUser).ToList();
        return userRaces;
    }

    public async Task<AppUser> GetAppUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetByIdNoTracking(string id)
    {
        return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Update(AppUser user)
    {
        _context.Update(user);
        return Save();
    }


}
