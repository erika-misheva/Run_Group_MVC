namespace RunGroupAplication.Interfaces;

using Models;
public interface IDashboardRepository
{
    Task<List<Race>> GetAllUserRaces();
    Task<List<Club>> GetAllUserClubs();
    Task<AppUser> GetAppUserById(string id);

    Task<AppUser> GetByIdNoTracking(string id);
    bool Save();
    bool Update(AppUser user);
}
