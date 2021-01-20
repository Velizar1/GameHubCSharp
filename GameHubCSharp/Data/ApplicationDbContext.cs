using GameHubCSharp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameHubCSharp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameEvent> GameEvents { get; set; }
        public DbSet<Player> Players { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

                

            builder.Entity<Player>()
                .HasMany(x => x.GameEvents)
                .WithMany(x => x.Players);

            


            base.OnModelCreating(builder);
        }

    }
}
