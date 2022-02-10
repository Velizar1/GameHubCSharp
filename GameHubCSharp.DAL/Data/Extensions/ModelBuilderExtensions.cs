using GameHubCSharp.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Extensions
{
    public static class ModelBuilderExtensions
    {

        public static void BuildPlayer(this ModelBuilder builder)
        {
            builder.Entity<Player>(player =>
            {
                player.HasMany(x => x.GameEvents)
                .WithMany(x => x.Players);


                player.HasOne(u => u.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            });
        }


        public static void BuildPost(this ModelBuilder builder)
        {
            builder.Entity<Post>(post =>
            {
                post.HasOne(a => a.Category)
                .WithMany(b => b.Posts)
                .OnDelete(DeleteBehavior.Cascade);

                post.HasOne(a => a.Category)
                    .WithMany(b => b.Posts);
            });
        }


        public static void BuildUser(this ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasMany(n => n.Notifications)
                .WithOne();
            });
        }
    }
}
