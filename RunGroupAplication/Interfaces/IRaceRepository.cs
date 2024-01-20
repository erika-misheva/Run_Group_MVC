namespace RunGroupAplication.Interfaces;

using Models;
public interface IRaceRepository
{
    Task<IEnumerable<Race>> GetAllRaces();

    Task<Race> GetByIdAsync(int id);

    Task<Race> GetByIdAsyncNoTracking(int id);

    Task<IEnumerable<Race>> GetRaceByCity(string city);

    bool AddRace(Race race);

    bool UpdateRace(Race race);

    bool DeleteRace(Race race);

    bool Save();
}
