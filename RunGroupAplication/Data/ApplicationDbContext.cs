using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace RunGroupAplication.Data;

using Models;
using RunningForce.Models;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
    {
            
    }

    public DbSet<Race> Races { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }  // Add DbSet for AppUser

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>()
                    .HasOne(c => c.AppUserCreator);
                    
        modelBuilder.Entity<AppUser>()
                    .HasMany(u => u.Clubs)
                    .WithMany(c => c.Members)
                    .UsingEntity(entityTypeBuilder => entityTypeBuilder.ToTable("AppUserClub"));

        base.OnModelCreating(modelBuilder);
    }
}
