using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        public Task Add(Category category);
        public Task Delete(Category category);
        public Task<List<Category>> FindAll();
        public Task<Category> FindByType(string type);
        public Task<Category> FindById(Guid id);
    }
}
