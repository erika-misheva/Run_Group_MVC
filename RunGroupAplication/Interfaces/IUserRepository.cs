namespace RunGroupAplication.Interfaces;

using Models;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAppUsers();
    Task<AppUser> GetAppUserById(string id);

    Task<List<Club>> GetAllUserClubs(string Id);
    Task<List<Race>> GetAllUserRaces(string Id);

    bool Add(AppUser user);

    bool Update(AppUser user);  

    bool Delete(AppUser user);

    bool Save();
}
