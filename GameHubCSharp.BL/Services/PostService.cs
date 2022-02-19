using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;

namespace GameHubCSharp.BL.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        public PostService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            await repository.CreateAsync(post);
            await repository.SaveChangesAsync();
            return post;
        }

        public int Count()
        {
            return repository
                .All<Post>()
                .Count();
        }

        public int Count(string category)
        {
            if (category == "")
            {
                return Count();
            }

            return repository
                .All<Post>(p => p.Category.Type == category)
                .Count()
        }

        public List<Post> FindAll(int index, int size, string category)
        {
            if (category != "")
            {
                return repository
                    .All<Post>(p => p.Category.Type == category)
                    .Skip((index - 1) * size)
                    .Take(size)
                    .ToList();
            }

            return repository
                .All<Post>()
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
        }

        public List<Post> FindAll()
        {
            return repository
                .All<Post>()
                .ToList();
        }

        public Post FindPostById(string Id)
        {
            return repository
                .All<Post>()
                .FirstOrDefault(p => p.Id.ToString() == Id);
        }

        public Post FindPostByTopic(string topic)
        {
            return repository
                .All<Post>()
                .FirstOrDefault(p => p.Topic == topic);
        }

        public ICollection<Post> FindPostsByCreator(User creator)
        {
            return repository
                .All<Post>(p => p.Creator.Id == creator.Id)
                .ToList();
        }


        public async Task RemoveAsync(Post post)
        {
            await repository.DeleteAsync(post);
            await repository.SaveChangesAsync();
        }

        public async Task RemovePostByIdAsync(string id)
        {
            var post = repository
                .All<Post>()
                .FirstOrDefault(post => post.Id.ToString() == id);

            await repository.DeleteAsync(post);
            await repository.SaveChangesAsync()
        }
    }
}
