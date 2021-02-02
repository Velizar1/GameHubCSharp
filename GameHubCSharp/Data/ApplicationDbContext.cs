using GameHubCSharp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameHubCSharp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameEvent> GameEvents { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Notification> Notifications { get; set; }
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

            builder.Entity<Player>(p =>
            {
                p.HasMany(x => x.GameEvents)
                .WithMany(x => x.Players);

                p.HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<Player>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<Post>()
             .HasOne(a => a.Category)
             .WithMany(b => b.Posts)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
             .HasOne(a => a.Category)
             .WithMany(b => b.Posts)
             ;
            builder.Entity<Notification>()
            .HasOne(a => a.From)
            .WithMany(b => b.Notifications);

           

            base.OnModelCreating(builder);
        }

    }
}
