using Microsoft.EntityFrameworkCore;


namespace RunGroupAplication.Repository;

using Data;
using Interfaces;
using Models;

public class RaceRepository : IRaceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RaceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;       
    }
    public bool AddRace(Race race)
    {
        _dbContext.Races.Add(race);
        return Save();
    }

    public bool DeleteRace(Race race)
    {
        _dbContext.Races.Remove(race);
        return Save();
    }

    public async Task<IEnumerable<Race>> GetAllRaces()
    {
        List<Race> races = await _dbContext.Races.ToListAsync();
        return races;
    }

    public async Task<Race> GetByIdAsync(int id)
    {
        Race race = await _dbContext.Races.Include(a => a.Address).Include(au => au.AppUser).FirstOrDefaultAsync(r => r.Id == id);
        return race;
    }
    public async Task<Race> GetByIdAsyncNoTracking(int id)
    {
        Race race = await _dbContext.Races.Include(a => a.Address).Include(au => au.AppUser).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        return race;
    }

    public async Task<IEnumerable<Race>> GetRaceByCity(string city)
    {
        List<Race> racesByCity = await _dbContext.Races.Where(r => r.Address.City.ToLower() == city).ToListAsync();
        return racesByCity;
    }

    public bool Save()
    {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UpdateRace(Race race)
    {
        _dbContext.Races.Update(race);
        return Save();
    }
}
