using Microsoft.EntityFrameworkCore;
using BeMo.Models;

namespace BeMo.Data
{
    public interface IBeMoContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Challenge> Challenge { get; set; }
        public DbSet<User_Challenge> User_Challenge { get; set; }
        DbContext Instance { get; }
    }
}
