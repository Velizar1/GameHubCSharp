﻿using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
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

        public int Count()
        {
            return db.Posts.Count();
        }

        public int Count(string category)
        {
            if (category == "")
            {
                return Count();
            }
            return db.Posts.Where(p=>p.Category.Type==category).Count();
        }

        public List<Post> FindAll(int index,int size,string category)
        {
            if (category != "")
            {
                return db.Posts.Where(p=>p.Category.Type==category).Skip((index - 1) * size).Take(size).ToList();
            }
            return db.Posts.Skip((index - 1) * size).Take(size).ToList();
        }

        public List<Post> FindAll()
        {
            return db.Posts.ToList();
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