using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IPostService : IBaseService
    {

        public Post FindPostById(Guid Id);
        public Post FindPostByTopic(string topic);
        public List<Post> FindPostsByCreator(User creator);
        public Task<Post> AddPostAsync(Post post);
        public Task RemovePostAsync(Post post);
        public Task DeleteAsync(Guid id);
        public List<Post> FindAll();
        public int Count(string category = "");
        public List<Post> FindAll(int? index, int pagesize, string category = "");
    }
}
