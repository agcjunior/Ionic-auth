using Microsoft.EntityFrameworkCore;

namespace authApi.Models
{
    public class AuthDbContext: DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserClaim> AppUserClaims { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
          :base(options)
        {            
        }

    }
}