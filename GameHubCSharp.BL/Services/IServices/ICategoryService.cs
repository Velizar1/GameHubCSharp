using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        public Task AddAsync(Category category);
        public Task DeleteAsync(Category category);
        public List<Category> FindAll();
        public Category FindByType(string type);
        public Category FindById(Guid id);
    }
}
