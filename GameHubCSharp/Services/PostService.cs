using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Post AddPost(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return post;
        }

        public Post FindPostById(string Id)
        {
            return db.Posts.FirstOrDefault(p => p.Id.ToString() == Id);
        }

        public Post FindPostByTopic(string topic)
        {
            return db.Posts.FirstOrDefault(p => p.Topic == topic);
        }

        public ICollection<Post> FindPostsByCreator(User creator)
        {
            return db.Posts.Where(p => p.Creator.Id == creator.Id).ToList();
        }

        public ICollection<Post> GetAllPosts()
        {
            return db.Posts.ToList();
        }

        public void RemovePost(Post post)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public void RemovePostById(string id)
        {
            var post = db.Posts.FirstOrDefault(post => post.Id.ToString() == id);
            db.Posts.Remove(post);
            db.SaveChanges();
        }
    }
}
