using Microsoft.EntityFrameworkCore;
using PROG6216_CMCS_POE_.Models.DBEntities;

namespace PROG6216_CMCS_POE_.DataAccesLayer
{
    public class ClaimsDbContext : DbContext
    {
        public ClaimsDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Claim> Claims { get; set; }
    }
}
