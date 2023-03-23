using Microsoft.EntityFrameworkCore;

namespace BeMo.Data
{
    public interface IBeMoContext
    {
        public DbSet<BeMo.Models.User> User { get; set; }
        public DbSet<BeMo.Models.Activity> Activity { get; set; }
        public DbSet<BeMo.Models.Admin> Admin { get; set; }
        public DbSet<BeMo.Models.Challenge> Challenge { get; set; }
        public DbSet<BeMo.Models.StravaCredentials> StravaCredentials { get; set; }
        DbContext Instance { get; }
    }
}
