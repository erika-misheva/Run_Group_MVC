namespace RunGroupAplication.Interfaces;

using Models;

public interface IClubRepository
{
   Task<IEnumerable<Club>> GetAllClubs();

    Task<Club> GetByIdAsync(int id);

    Task<Club> GetByIdAsyncNoTracking(int id);

    Task<IEnumerable<Club>> GetClubByCity(string city);

    Task<bool> AddMemberToClub(int clubId);

    bool AddClub (Club club);

    bool UpdateClub (Club club);

    bool DeleteClub (Club club);

    bool Save();
}
