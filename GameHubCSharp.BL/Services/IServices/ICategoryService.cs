using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface ICategoryService
    {
        public Task AddAsync(Category category);
        public Task DeleteAsync(string id);
        public List<Category> FindAll();
        public Category FindByName(string type);
        public Category FindById(string id);
    }
}
