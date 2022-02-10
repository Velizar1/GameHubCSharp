using GameHubCSharp.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Helpers
{
    public class ModelBuilderHelper
    {

        private static void BuildPlayer(ModelBuilder builder)
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


        private static void BuildPost(ModelBuilder builder)
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


        private static void BuildUser(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasMany(n => n.Notifications)
                .WithOne();
            });
        }


        public static void Build(ModelBuilder builder)
        {
            BuildPlayer(builder);
            BuildPost(builder);
            BuildUser(builder);
        }
    }
}
