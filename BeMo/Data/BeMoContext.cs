using BeMo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BeMo.Data
{
    public class BeMoContext : DbContext, IBeMoContext
    {
        public BeMoContext(DbContextOptions<BeMoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Challenge>()
                .HasOne(u => u.User)
                .WithMany(uc => uc.User_Challenges)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<User_Challenge>()
               .HasOne(c => c.Challenge)
               .WithMany(uc => uc.User_Challenges)
               .HasForeignKey(ci => ci.ChallengeId);
        }

        public DbContext Instance => this;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Activity> Activity { get; set; } = default!;
        public DbSet<Challenge> Challenge { get; set; } = default!;
        public DbSet<User_Challenge> User_Challenge { get; set; } = default!;
    }
}
