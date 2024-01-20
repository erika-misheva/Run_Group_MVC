using Microsoft.EntityFrameworkCore;

namespace RunGroupAplication.Repository;

using Data;
using Interfaces;
using Models;

public class ClubRepository : IClubRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClubRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;

    }
    public bool AddClub(Club club)
    {
        _dbContext.Clubs.Add(club);
        return Save();
    }

    public bool DeleteClub(Club club)
    {
        _dbContext.Clubs.Remove(club);
        return Save();
    }

    public async Task<IEnumerable<Club>> GetAllClubs()
    {
        List<Club> clubs = await _dbContext.Clubs.ToListAsync();
        return clubs;
    }

    public async Task<Club> GetByIdAsync(int id)
    {
        Club club = await _dbContext.Clubs.Include(a => a.Address).Include(c=> c.Members).FirstOrDefaultAsync(c => c.Id == id);
        return club;
    }

    public async Task<Club> GetByIdAsyncNoTracking(int id)
    {
        Club club = await _dbContext.Clubs.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return club;
    }

    public async Task<IEnumerable<Club>> GetClubByCity(string city)
    {
        List<Club> clubsByCity = await _dbContext.Clubs.Where(c => c.Address.City.ToLower() == city.ToLower()).ToListAsync();
        return clubsByCity;
    }

    public async Task<bool> AddMemberToClub(int clubId)
    {
        string? curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
        if (string.IsNullOrWhiteSpace(curUserId))
        {
            return false;
        }

        AppUser? curUser = _dbContext.Users
        .Include(u => u.Clubs)
        .FirstOrDefault(u => u.Id == curUserId);

        if (curUser is null)
        {
            return false;
        }

        Club club = await GetByIdAsync(clubId);

        var Clubs = curUser.Clubs;

        var clubExits = curUser.Clubs.FirstOrDefault(c => c.Id == clubId);

        if (clubExits is null)
        {
            club.Members.Add(curUser);
        }

        return Save();
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UpdateClub(Club club)
    {
        _dbContext.Update(club);
        return Save();
    }



}
