﻿using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services.IServices
{
    public interface IPostService
    {
        public ICollection<Post> GetAllPosts();
        public Post FindPostById(string Id);
        public Post FindPostByTopic(string topic);
        public ICollection<Post> FindPostsByCreator(User creator);
        public Post AddPost(Post post);
        public void RemovePost(Post post);
        public void RemovePostById(string id);
        public List<Post> FindAll();
    }
}