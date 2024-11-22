using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG6216_CMCS_POE_.Models.DBEntities;

namespace PROG6216_CMCS_POE_.DataAccesLayer
{
    public class ClaimsDbContext : IdentityDbContext<IdentityUser>
    {
        public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; } // Existing Claim table

        // Add any additional DbSet properties here
    }
}
