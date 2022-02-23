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
    public class PostService : BaseService, IPostService
    {

        public PostService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Post> AddPostAsync(Post post)
        {
            await repository.CreateAsync(post);
            return post;
        }

        public int Count(string categoryType = "")
        {
            return repository.AllReadOnly<Post>(p => p.Category.Type == (categoryType == "" ? p.Category.Type : categoryType))
                .Count();
        }

        public List<Post> FindAll(int? index, int size, string category = "")
        {
            return repository.AllReadOnly<Post>(p => p.Category.Type == (category == "" ? p.Category.Type : category))
                .Skip(((index ?? 1) - 1) * size)
                .Take(size)
                .ToList();
        }

        public List<Post> FindAll()
        {
            return repository.AllReadOnly<Post>()
                .ToList();
        }

        public Post FindPostById(Guid Id)
        {
            return repository.AllReadOnly<Post>()
                .FirstOrDefault(p => p.Id == Id);
        }

        public Post FindPostByTopic(string topic)
        {
            return repository.AllReadOnly<Post>()
                .FirstOrDefault(p => p.Topic == topic);
        }

        public List<Post> FindPostsByCreator(User creator)
        {
            return repository.AllReadOnly<Post>(p => p.Creator.Id == creator.Id)
                .ToList();
        }

        public async Task RemovePostAsync(Post post)
        {
            await repository.DeleteAsync(post);
        }

        public async Task RemovePostByIdAsync(Guid id)
        {
            var post = repository.All<Post>()
                .FirstOrDefault(post => post.Id == id);

            await repository.DeleteAsync(post);
        }

    }
}
