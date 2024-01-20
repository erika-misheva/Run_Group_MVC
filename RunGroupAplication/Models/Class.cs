using RunGroupAplication.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RunningForce.Models
{
    public class AppUserClub
    {
        public Club Club { get; set; }
        public AppUser AppUser { get; set; }
        [Key]
        public string AppUserId { get; set; }
        [Key]
        public int ClubId { get; set; }
    }
}
