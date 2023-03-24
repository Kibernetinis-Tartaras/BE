﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BeMo.Data
{
    public class BeMoContext : DbContext, IBeMoContext
    {
        public BeMoContext(DbContextOptions<BeMoContext> options) : base(options)
        {
        }

        public DbContext Instance => this;
        public DbSet<BeMo.Models.User> User { get; set; } = default!;
        public DbSet<BeMo.Models.Activity> Activity { get; set; } = default!;
        public DbSet<BeMo.Models.Challenge> Challenge { get; set; } = default!;
    }
}
