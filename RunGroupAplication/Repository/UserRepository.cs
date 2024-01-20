using Microsoft.EntityFrameworkCore;

namespace RunGroupAplication.Repository;

using Data;
using Interfaces;
using Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public bool Add(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser> GetAppUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<AppUser>> GetAppUsers()
    {
        return await _context.Users.ToListAsync();
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

    public async Task<List<Club>> GetAllUserClubs(string userId)
    {
        var clubsCreatedByUser = _context.Clubs.Where(c => c.AppUserCreator.Id == userId);

        var clubsWithUser = _context.Clubs
            .Where(c => c.Members.Any(m => m.Id == userId))
            .Union(clubsCreatedByUser)
            .ToList();

        return clubsWithUser;
    }

    public async Task<List<Race>> GetAllUserRaces(string Id)
    {
        
        var userRaces = _context.Races.Where(c => c.AppUser.Id == Id).ToList();
        return userRaces;
    }
}
